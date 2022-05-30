package com.niklyadov.hovyfridge.data.api

import okhttp3.Interceptor
import okhttp3.Response

class ApiInterceptor (
    val authToken : String,
    val product : String,
    val productVersion : String,
    val platformDetails : String
): Interceptor {

    override fun intercept(chain: Interceptor.Chain): Response {
        val request = chain.request()
            .newBuilder()
            .addHeader("Authorization", "Bearer $authToken")
            .addHeader("User-Agent", "$product/$productVersion Android ($platformDetails)")
            .build()

        return chain.proceed(request)
    }
}