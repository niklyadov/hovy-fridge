package com.niklyadov.hovyfridge

import android.app.Application
import com.niklyadov.hovyfridge.data.entities.VersionInfo
import dagger.hilt.android.HiltAndroidApp

@HiltAndroidApp
class App : Application() {
    companion object {
        val VERSION_INFO : VersionInfo = VersionInfo("", 1, 0, "Fresh fish", "")
        const val DEFAULT_API_URL : String = "http://hovyfridge.lyadov.net:28666/"
    }
}