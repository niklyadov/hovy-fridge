package com.niklyadov.hovyfridge.services

import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.data.repository.FridgesRepository
import javax.inject.Inject
import kotlin.Throwable

class FridgesServiceImpl @Inject constructor(
    private val fridgeRepository : FridgesRepository
) : FridgesService {
    override suspend fun getFridges(): Result<List<Fridge>> {
        try {

            val fridgesList = fridgeRepository.getFridgesList()

            return Result.success(fridgesList)

        } catch (ex : Throwable) {
            return Result.failure(ex)
        }
    }

    override suspend fun getFridge(fridgeId: Int): Result<Fridge> {
        try {

            val fridge = fridgeRepository.getFridge(fridgeId)

            return Result.success(fridge)

        } catch (ex : Throwable) {
            return Result.failure(ex)
        }
    }

    override suspend fun updateFridge(fridge: Fridge): Result<Fridge> {
        try {

            val updatedFridge = fridgeRepository.updateFridge(fridge)

            return Result.success(updatedFridge)

        } catch (ex : Throwable) {
            return Result.failure(ex)
        }
    }

    override suspend fun deleteFridge(fridgeId: Int): Result<Fridge> {
        try {

            val deletedFridge = fridgeRepository.deleteFridge(fridgeId)

            return Result.success(deletedFridge)

        } catch (ex : Throwable) {

            return Result.failure(ex)

        }
    }

    override suspend fun restoreFridge(fridgeId: Int): Result<Fridge> {
        try {

            val restoredFridge = fridgeRepository.restoreFridge(fridgeId)

            return Result.success(restoredFridge)

        } catch (ex : Throwable) {

            return Result.failure(ex)

        }
    }

    override suspend fun putProductIntoFridge(fridgeId: Int, product: Product) : Result<Product> {
        try {

            val addedProduct = fridgeRepository.putProductIntoFridge(fridgeId, product.id)

            return Result.success(addedProduct)

        } catch (ex : Throwable) {

            return Result.failure(ex)

        }
    }

    override suspend fun removeProductFromFridge(fridgeId: Int, product: Product) : Result<Product> {
        try {

            val removedProduct = fridgeRepository.removeProductFromFridge(fridgeId, product.id)

            return Result.success(removedProduct)

        } catch (ex : Throwable) {

            return Result.failure(ex)

        }
    }

    override fun isFridgeContainsProductWithBarcode(fridgeId: Int, productBarCode : String) : Boolean {
        return false
    }

    override suspend fun addNewFridge(fridge: Fridge) : Result<Fridge> {
        try {
            val addedFridge = fridgeRepository.addFridge(fridge)

            return Result.success(addedFridge)

        } catch (ex : Throwable) {

            return Result.failure(ex)

        }
    }

    override suspend fun renameFridge(fridgeId: Int, fridgeName: String): Result<Fridge> {
        try {

            val fridge = fridgeRepository.renameFridge(fridgeId, fridgeName)

            return Result.success(fridge)

        } catch (ex : Throwable) {
            return Result.failure(ex)
        }
    }

    override suspend fun restoreProductInFridge(fridgeId: Int, product: Product): Result<Product> {
        try {

            val removedProduct = fridgeRepository.restoreProductInFridge(fridgeId, product.id)

            return Result.success(removedProduct)

        } catch (ex : Throwable) {

            return Result.failure(ex)

        }
    }
}