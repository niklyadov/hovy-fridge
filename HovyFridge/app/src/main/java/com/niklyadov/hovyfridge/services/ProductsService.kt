package com.niklyadov.hovyfridge.services

import com.niklyadov.hovyfridge.data.entities.Product

interface ProductsService {
    suspend fun getProductsList(queryString: String?) : Result<List<Product>>

    suspend fun updateProduct(product : Product) : Result<Product>

    suspend fun getProduct(productId : Int) : Result<Product>

    suspend fun deleteProduct(productId : Int) : Result<Product>

    suspend fun restoreProduct(productId: Int) : Result<Product>

    suspend fun checkIsProductExistsByBarcode(barcode : String) : Boolean

    suspend fun addProductToList(product: Product) : Result<Product>

    suspend fun getProductByBarcode(barcode : String) : Result<Product>

    suspend fun renameProduct(id: Int, productName: String): Result<Product>

    suspend fun changeAmountOfProduct(id: Int, newAmount: Short): Result<Product>
}