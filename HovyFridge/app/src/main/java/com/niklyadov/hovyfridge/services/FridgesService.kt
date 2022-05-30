package com.niklyadov.hovyfridge.services

import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product

interface FridgesService {
    suspend fun getFridges(): Result<List<Fridge>>

    suspend fun getFridge(fridgeId : Int) : Result<Fridge>

    suspend fun updateFridge(fridge : Fridge) : Result<Fridge>

    suspend fun deleteFridge(fridgeId : Int) : Result<Fridge>

    suspend fun restoreFridge(fridgeId: Int) : Result<Fridge>

    suspend fun putProductIntoFridge(fridgeId: Int, product: Product) : Result<Product>

    suspend fun removeProductFromFridge(fridgeId: Int, product: Product) : Result<Product>

    fun isFridgeContainsProductWithBarcode(fridgeId: Int, productBarCode : String) : Boolean

    suspend fun addNewFridge(fridge: Fridge) : Result<Fridge>

    suspend fun renameFridge(_fridgeId: Int, fridgeName: String) : Result<Fridge>

    suspend fun restoreProductInFridge(_fridgeId: Int, item: Product): Result<Product>
}