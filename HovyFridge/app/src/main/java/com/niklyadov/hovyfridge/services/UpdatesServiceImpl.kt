package com.niklyadov.hovyfridge.services

import android.app.DownloadManager
import android.content.Context
import android.content.Context.DOWNLOAD_SERVICE
import android.net.Uri
import android.os.Environment
import com.niklyadov.hovyfridge.App
import com.niklyadov.hovyfridge.data.api.RetrofitServices
import com.niklyadov.hovyfridge.data.entities.isGreaterThan
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject


class UpdatesServiceImpl @Inject constructor(
    private val apiServices: RetrofitServices,
    @ApplicationContext private val context: Context
) : UpdatesService {
    override suspend fun downloadUpdate(): Result<Boolean> {
        try {
            val versionInfo = apiServices.getLastUpdateInfo()

            if(!versionInfo.isGreaterThan(App.VERSION_INFO)) {
                return Result.success(false)
            }

            val versionString = "v.${versionInfo.majorNumber}.${versionInfo.minorNumber}"
            val downloadID = downloadFile (
                "hovyfridge_$versionString.apk",
                "Hovy Fridge $versionString ${versionInfo.name} Update",
                versionInfo.downloadUrl
            )

            return Result.success(true)

        } catch (ex : Exception) {
            return Result.failure(ex)
        }
    }

    private fun downloadFile(fileName : String, desc :String, url : String) : Long {
        val request = DownloadManager.Request(Uri.parse(url))
            .setAllowedNetworkTypes(DownloadManager.Request.NETWORK_WIFI or DownloadManager.Request.NETWORK_MOBILE)
            .setTitle(fileName)
            .setDescription(desc)
            .setNotificationVisibility(DownloadManager.Request.VISIBILITY_VISIBLE_NOTIFY_COMPLETED)
            .setAllowedOverMetered(true)
            .setAllowedOverRoaming(false)
            .setDestinationInExternalPublicDir(Environment.DIRECTORY_DOWNLOADS, fileName)
        val downloadManager = context.getSystemService(DOWNLOAD_SERVICE) as DownloadManager
        return downloadManager.enqueue(request)
    }
}