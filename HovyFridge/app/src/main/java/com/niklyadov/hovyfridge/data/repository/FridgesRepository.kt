package com.niklyadov.hovyfridge.data.repository

import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product

interface FridgesRepository {
    suspend fun getFridge(id : Int) : Fridge?

    suspend fun updateFridge(fridge : Fridge) : Fridge

    suspend fun deleteFridge(id : Int) : Fridge

    suspend fun restoreFridge(id : Int): Fridge

    suspend fun putProductIntoFridge(fridgeId : Int, productId: Int) : Product

    suspend fun removeProductFromFridge(fridgeId : Int, productId: Int) : Product

    suspend fun getFridgesList() : List<Fridge>

    suspend fun addFridge(fridge: Fridge) : Fridge

    suspend fun renameFridge(fridgeId: Int, fridgeName: String): Fridge?

    suspend fun restoreProductInFridge(fridgeId: Int, productId: Int): Product?
}