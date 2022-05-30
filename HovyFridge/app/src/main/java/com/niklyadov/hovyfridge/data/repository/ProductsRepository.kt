package com.niklyadov.hovyfridge.data.repository

import com.niklyadov.hovyfridge.data.entities.Product

interface ProductsRepository {
    suspend fun getProductsList(): List<Product>

    suspend fun getProductWithBarcode(barcode : String): Product

    suspend fun getProduct(id : Int): Product

    suspend fun updateProduct(product: Product): Product

    suspend fun deleteProduct(id : Int): Product

    suspend fun restoreProduct(id : Int): Product

    suspend fun addProductToList(product: Product) : Product
}