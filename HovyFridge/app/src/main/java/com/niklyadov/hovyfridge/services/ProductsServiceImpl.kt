package com.niklyadov.hovyfridge.services

import android.util.Log
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.data.repository.ProductsRepository
import java.util.*
import javax.inject.Inject
import kotlin.Exception

class ProductsServiceImpl @Inject constructor(
    private val productsRepository: ProductsRepository
    ) : ProductsService  {

    override suspend fun getProductsList(queryString: String?) : Result<List<Product>> {
        try {
            var products = productsRepository.getProductsList().toMutableList()
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
            val updatedProduct = productsRepository.updateProduct(product)

            return Result.success(updatedProduct)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun getProduct(productId: Int): Result<Product> {
        try {
            val product = productsRepository.getProduct(productId)
            return Result.success(product)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun deleteProduct(productId: Int): Result<Product> {
        try {
            val deletedProduct = productsRepository.deleteProduct(productId)

            return Result.success(deletedProduct)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun restoreProduct(productId: Int): Result<Product> {
        try {
            val restoredProduct = productsRepository.restoreProduct(productId)

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
            val addedProduct = productsRepository.addProductToList(product)

            return Result.success(addedProduct)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun getProductByBarcode(barcode : String) : Result<Product> {
        try {
            val product = productsRepository.getProductWithBarcode(barcode)
            if(product == null) {
                return Result.failure(Exception("Product was not found"))
            }

            return Result.success(product)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun renameProduct(id: Int, productName: String): Result<Product> {
        try {
            val product = productsRepository.getProduct(id)
            product.name = productName;
            return Result.success(productsRepository.updateProduct(product))

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    override suspend fun changeAmountOfProduct(id: Int, newAmount: Short): Result<Product> {
        try {
            if (newAmount < 0 ) {
                return Result.failure(Exception("Amount of product should be greater than zero. Current value: $newAmount"))
            }

            val product = productsRepository.getProduct(id)
            product.amount = newAmount;
            return Result.success(productsRepository.updateProduct(product))

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }
}