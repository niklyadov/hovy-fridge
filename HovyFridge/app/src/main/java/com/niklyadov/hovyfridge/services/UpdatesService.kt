package com.niklyadov.hovyfridge.services

interface UpdatesService {
    suspend fun downloadUpdate(versionId : String) : Result<Boolean>
}