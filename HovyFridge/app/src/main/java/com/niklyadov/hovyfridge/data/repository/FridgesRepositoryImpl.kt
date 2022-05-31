package com.niklyadov.hovyfridge.data.repository

import com.niklyadov.hovyfridge.data.api.RetrofitServices
import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.data.entities.relations.toFridge
import com.niklyadov.hovyfridge.di.FridgeDao
import javax.inject.Inject

class FridgesRepositoryImpl @Inject constructor(
    private val apiService : RetrofitServices,
    private val fridgeDao: FridgeDao
)  : FridgesRepository {
    override suspend fun getFridge(id: Int): Fridge {
        try {

            val fridge = apiService.getFridge(id)

            fridgeDao.insertFridgeWithProducts(fridge)
            return fridge

        }
        catch (ex : Exception) {
            return fridgeDao.getFridgeWithProducts(id)!!
        }
    }

    override suspend fun updateFridge(fridge: Fridge): Fridge {
        return apiService.updateFridge(fridge)
    }

    override suspend fun deleteFridge(id: Int): Fridge {
        return apiService.deleteFridge(id)
    }

    override suspend fun restoreFridge(id: Int): Fridge {
        return apiService.restoreFridge(id)
    }

    override suspend fun putProductIntoFridge(fridgeId: Int, productId: Int): Product {
        return apiService.putProductIntoFridge(fridgeId,productId)
    }

    override suspend fun removeProductFromFridge(fridgeId: Int, productId: Int): Product {
        return apiService.removeProductFromFridge(fridgeId, productId)
    }

    override suspend fun getFridgesList(): List<Fridge> {
        try {

            val fridges = apiService.getFridgesList()
            fridgeDao.insertFridgesWithProducts(fridges)

            return fridges

        } catch (ex : Exception) {

            return fridgeDao.getFridgesWithProducts()

        }
    }

    override suspend fun addFridge(fridge: Fridge): Fridge {
        return apiService.addFridge(fridge)
    }

    override suspend fun renameFridge(fridgeId: Int, fridgeName: String): Fridge {
        val fridge = getFridge(fridgeId)
            fridge.name = fridgeName;
        return updateFridge(fridge)
    }

    override suspend fun restoreProductInFridge(fridgeId: Int, productId: Int): Product {
        return  apiService.restoreProductInFridge(fridgeId, productId)
    }
}