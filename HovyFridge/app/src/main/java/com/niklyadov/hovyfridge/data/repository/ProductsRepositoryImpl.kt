package com.niklyadov.hovyfridge.data.repository

import com.niklyadov.hovyfridge.data.api.RetrofitServices
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.di.ProductDao
import java.lang.Exception
import javax.inject.Inject

class ProductsRepositoryImpl @Inject constructor(
    private val apiService : RetrofitServices,
    private val productDao: ProductDao
)  : ProductsRepository {

    override suspend fun getProductsList(): List<Product> {
        try {

            val products = apiService.getProductsList()

            productDao.insertAll(products)

            return products
        } catch (ex : Exception) {

            return productDao.getAll()

        }
    }

    override suspend fun getProductWithBarcode(barcode: String): Product {
        return apiService.getProductWithBarcode(barcode)
    }

    override suspend fun getProduct(id: Int): Product {
        try {

            val product = apiService.getProduct(id)

            productDao.insert(product)

            return product

        } catch (ex : Exception) {

            return productDao.getById(id)

        }
    }

    override suspend fun updateProduct(product: Product): Product {
        return apiService.updateProduct(product)
    }

    override suspend fun deleteProduct(id: Int): Product {
        return apiService.deleteProduct(id)
    }

    override suspend fun restoreProduct(id: Int): Product {
        return apiService.restoreProduct(id)
    }

    override suspend fun addProductToList(product: Product): Product {
        return apiService.addProductToList(product)
    }
}