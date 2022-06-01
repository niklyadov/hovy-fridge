package com.niklyadov.hovyfridge.extensions

import retrofit2.Response

fun <T> Response<T>.toDetailedString() : String
    = "${code()} (${message()})  ---->  ${errorBody()?.string()} <---- Total: ${toString()}, headers: ${headers()}"