package com.niklyadov.hovyfridge.data.api

import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.data.entities.VersionInfo
import retrofit2.Call
import retrofit2.Response
import retrofit2.http.*

interface RetrofitServices {
    @GET("products/")
    suspend fun getProductsList(): List<Product>

    @GET("products/barcode/{barcode}")
    suspend fun getProductWithBarcode(@Path("barcode") barcode : String): Product

    @GET("products/{id}")
    suspend fun getProduct(@Path("id") id : Int): Product

    @PUT("products")
    suspend fun updateProduct(@Body product: Product) : Product

    @DELETE("products/{id}")
    suspend fun deleteProduct(@Path("id") id : Int) : Product

    @PUT("products/{id}/restore")
    suspend fun restoreProduct(@Path("id") id : Int) : Product

    @POST("products/")
    suspend fun addProductToList(@Body product: Product) : Product

    @GET("fridges/{id}")
    suspend fun getFridge(@Path("id") id : Int) : Response<Fridge>

    @PUT("fridges/")
    suspend fun updateFridge(@Body fridge : Fridge) : Response<Fridge>

    @DELETE("fridges/{id}")
    suspend fun deleteFridge(@Path("id") id : Int) : Response<Fridge>

    @PUT("fridge/{id}/restore")
    suspend fun restoreFridge(@Path("id") id : Int) : Response<Fridge>

    @PUT("fridges/{id}/product")
    suspend fun putProductIntoFridge(@Path("id") fridgeId : Int, @Body productId: Int) : Response<Product>

    @PUT("fridges/{id}/product/restore")
    suspend fun restoreProductInFridge(@Path("id") fridgeId : Int, @Body productId: Int) : Response<Product>

    @DELETE("fridges/{id}/product/{productId}")
    suspend fun removeProductFromFridge(@Path("id") fridgeId : Int, @Path("productId") productId: Int) : Response<Product>

    @GET("fridges/")
    suspend fun getFridgesList() : Response<List<Fridge>>

    @POST("fridges/")
    suspend fun addFridge(@Body fridge: Fridge) : Response<Fridge>

    @GET("versions/{id}")
    suspend fun getUpdateInfo(@Path("id") versionId : String) : VersionInfo

    @GET("versions/last")
    suspend fun getLastUpdateInfo() : VersionInfo
}