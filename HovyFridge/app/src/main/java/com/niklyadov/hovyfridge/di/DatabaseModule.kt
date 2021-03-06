package com.niklyadov.hovyfridge.di

import android.content.Context
import androidx.room.*
import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.data.entities.relations.FridgeWithProducts
import com.niklyadov.hovyfridge.data.entities.relations.toFridge
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent

@Module
@InstallIn(SingletonComponent::class)
internal class DatabaseModule {
    @Provides
    fun provideAppDatabase(@ApplicationContext context: Context)
            : AppDatabase = Room.databaseBuilder(
        context,
        AppDatabase::class.java, "app_db"
    ).allowMainThreadQueries().build()

    @Provides
    fun provideFridgesDao(appDatabase: AppDatabase) : FridgeDao
            = appDatabase.fridgeDao()

    @Provides
    fun provideProductsDao(appDatabase: AppDatabase) : ProductDao
            = appDatabase.productDao()

}

@Database(entities = [Fridge::class, Product::class], version = 4, exportSchema = false)
abstract class AppDatabase : RoomDatabase() {
    abstract fun productDao(): ProductDao
    abstract fun fridgeDao(): FridgeDao
}

@Dao
interface ProductDao {
    @Query("SELECT * FROM Product WHERE isDeleted = 0")
    fun getAll(): List<Product>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun insertAll(products: List<Product>)

    @Query("SELECT * FROM Product WHERE id = :id and isDeleted = 0")
    fun getById(id : Int): Product?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun insert(product: Product)
}

@Dao
interface FridgeDao {
    @Transaction
    @Query("SELECT * FROM Fridge WHERE isDeleted = 0")
    fun getAll(): List<FridgeWithProducts>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun insertAll(fridges: List<Fridge>)

    @Transaction
    @Query("SELECT * FROM Fridge WHERE id = :id AND isDeleted = 0")
    fun getById(id : Int): FridgeWithProducts

    @Query("SELECT * FROM Fridge WHERE id = :id AND isDeleted = 0")
    fun getByFridgeId(id : Int): Fridge?


    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun insert(fridge: Fridge)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun insertAllProducts(products: List<Product>)

    @Query("SELECT * FROM Product WHERE fridge_id =:fridgeId AND isDeleted = 0")
    fun getProductsList(fridgeId: Int): List<Product>

    fun insertFridgeWithProducts(user: Fridge) {
        val fridge: List<Product> = user.products
        for (i in fridge.indices) {
            fridge[i].fridgeId = user.id
        }
        insertAllProducts(fridge)
        insert(user)
    }

    fun getFridgeWithProducts(id: Int): Fridge? {
        getByFridgeId(id)?.let {
            val products: List<Product> = getProductsList(id)
            it.products = products
            return it
        }

        throw Exception("Fridge with id $id is not found")
    }

    fun insertFridgesWithProducts(fridges: List<Fridge>) {
        for (fridge in fridges) {
            insertFridgeWithProducts(fridge)
        }
    }
    fun getFridgesWithProducts():  List<Fridge>? {
        val fridges = getAll().map {
            fridgeWithProducts -> fridgeWithProducts.toFridge()
        }

        fridges.map {
            fridge -> getFridgeWithProducts(fridge.id)
        }

        return fridges;
    }
}