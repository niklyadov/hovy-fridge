package com.niklyadov.hovyfridge.data.entities

data class VersionInfo(
    val id : String,
    val majorNumber: Int,
    val minorNumber: Int,
    val name: String,
    val downloadUrl : String
)

fun VersionInfo.isGreaterThan(versionInfo: VersionInfo)
        = majorNumber >=  versionInfo.majorNumber && minorNumber > versionInfo.minorNumber