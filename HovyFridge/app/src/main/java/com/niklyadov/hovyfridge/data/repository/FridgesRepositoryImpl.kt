package com.niklyadov.hovyfridge.data.repository

import com.niklyadov.hovyfridge.data.api.RetrofitServices
import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.extensions.toDetailedString
import com.niklyadov.hovyfridge.di.FridgeDao
import java.io.IOException
import javax.inject.Inject

class FridgesRepositoryImpl @Inject constructor(
    private val apiService : RetrofitServices,
    private val fridgeDao: FridgeDao
)  : FridgesRepository {
    override suspend fun getFridge(id: Int): Fridge? {
        try {
            val fridgeResponse = apiService.getFridge(id)

            if (fridgeResponse.isSuccessful) {
                fridgeResponse.body()?.let {
                    fridgeDao.insertFridgeWithProducts(it)
                    return it
                }
            }

            throw Exception("Can`t get fridge with id $id, ${fridgeResponse.toDetailedString()}")

        } catch (ex : IOException) {
            fridgeDao.getByFridgeId(id)?.let {
                return it
            }
        }

        return null
    }

    override suspend fun updateFridge(fridge: Fridge): Fridge {
        val updateFridgeResponse = apiService.updateFridge(fridge)

        if (updateFridgeResponse.isSuccessful) {
            updateFridgeResponse.body()?.let {
                return it
            }
        }

        throw Exception("Can`t update fridge with id ${fridge.id}, " +
                updateFridgeResponse.toDetailedString()
        )
    }

    override suspend fun deleteFridge(id: Int): Fridge {
        val deleteFridgeResponse = apiService.deleteFridge(id)

        if(deleteFridgeResponse.isSuccessful) {
            deleteFridgeResponse.body()?.let {
                return it
            }
        }

        throw Exception("Can`t delete fridge with id ${id}, " +
                deleteFridgeResponse.toDetailedString()
        )
    }

    override suspend fun restoreFridge(id: Int): Fridge {
        val restoreFridgeResponse = apiService.restoreFridge(id)

        if(restoreFridgeResponse.isSuccessful) {
            restoreFridgeResponse.body()?.let {
                return it
            }
        }

        throw Exception("Can`t restore fridge with id ${id}, " +
                restoreFridgeResponse.toDetailedString()
        )
    }

    override suspend fun putProductIntoFridge(fridgeId: Int, productId: Int): Product {
        val putProductResponse = apiService.putProductIntoFridge(fridgeId,productId)

        if(putProductResponse.isSuccessful) {
            putProductResponse.body()?.let {
                return it
            }
        }

        throw Exception("Can`t put product with id $productId into fridge with id $fridgeId, " +
                putProductResponse.toDetailedString()
        )
    }

    override suspend fun removeProductFromFridge(fridgeId: Int, productId: Int): Product {
        val removeProductResponse = apiService.removeProductFromFridge(fridgeId, productId)

        if(removeProductResponse.isSuccessful) {
            removeProductResponse.body()?.let {
                return it
            }
        }

        throw Exception("Can`t remove product with id $productId into fridge with id ${fridgeId}, " +
                removeProductResponse.toDetailedString()
        )
    }

    override suspend fun restoreProductInFridge(fridgeId: Int, productId: Int): Product {
        val restoreProductResponse = apiService.restoreProductInFridge(fridgeId, productId)

        if(restoreProductResponse.isSuccessful) {
            restoreProductResponse.body()?.let {
                return it
            }
        }

        throw Exception("Can`t restore product with id $productId in fridge with id ${fridgeId}, " +
                restoreProductResponse.toDetailedString()
        )
    }

    override suspend fun getFridgesList(): List<Fridge> {
        try {
            val fridgeResponse = apiService.getFridgesList()

            if (fridgeResponse.isSuccessful) {
                fridgeResponse.body()?.let {
                    fridgeDao.insertFridgesWithProducts(it)
                    return it
                }
            }

            throw Exception("Can`t get fridges list, " +
                    fridgeResponse.toDetailedString()
            )

        } catch (ex : IOException) {
            fridgeDao.getFridgesWithProducts()?.let {
                return it
            }
        }

        return listOf()
    }

    override suspend fun addFridge(fridge: Fridge): Fridge {
        val addFridge = apiService.addFridge(fridge)

        if(addFridge.isSuccessful) {
            addFridge.body()?.let {
                return it
            }
        }

        throw Exception("Can`t add fridge ($fridge) " +
                addFridge.toDetailedString()
        )
    }

    override suspend fun renameFridge(fridgeId: Int, fridgeName: String): Fridge {
        getFridge(fridgeId)?.let {
            it.name = fridgeName
            return updateFridge(it)
        }

        throw Exception("Fridge with id $fridgeId is not found!")
    }
}