package com.niklyadov.hovyfridge.services

interface UpdatesService {
    suspend fun downloadUpdate() : Result<Boolean>
}