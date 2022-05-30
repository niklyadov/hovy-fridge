package com.niklyadov.hovyfridge.services

import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.data.repository.ProductsRepository
import java.util.*
import javax.inject.Inject
import kotlin.Exception

class ProductsServiceImpl @Inject constructor(
    private val productsService: ProductsRepository
    ) : ProductsService  {

    override suspend fun getProductsList(queryString: String?) : Result<List<Product>> {
        try {
            var products = productsService.getProductsList().toMutableList()
            if(queryString != null) {
                val queryStringPrepared = queryString.toLowerCase(Locale.ROOT).trim()

                products = products.filter {
                        p -> p.name.contains(queryStringPrepared) ||
                        p.barcode.contains(queryStringPrepared)
                } as MutableList<Product>
            }

            return Result.success(products)
        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun updateProduct(product: Product): Result<Product> {
        try {
            val updatedProduct = productsService.updateProduct(product)

            return Result.success(updatedProduct)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun getProduct(productId: Int): Result<Product> {
        try {
            val product = productsService.getProduct(productId)
            return Result.success(product)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun deleteProduct(productId: Int): Result<Product> {
        try {
            val deletedProduct = productsService.deleteProduct(productId)

            return Result.success(deletedProduct)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun restoreProduct(productId: Int): Result<Product> {
        try {
            val restoredProduct = productsService.restoreProduct(productId)

            return Result.success(restoredProduct)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun checkIsProductExistsByBarcode(barcode : String) : Boolean {
        return getProductByBarcode(barcode).isSuccess
    }

    override suspend fun addProductToList(product: Product) : Result<Product> {
        try {
            val addedProduct = productsService.addProductToList(product)

            return Result.success(addedProduct)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun getProductByBarcode(barcode : String) : Result<Product> {
        try {
            val product = productsService.getProductWithBarcode(barcode)

            return Result.success(product)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }
}