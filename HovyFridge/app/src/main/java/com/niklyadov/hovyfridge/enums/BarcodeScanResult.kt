package com.niklyadov.hovyfridge.enums

enum class BarcodeScanResult {
    ProductWasFound,
    ProductWasNotFound,
    ProductWasSuccessfulAddedIntoFridge,
    ProductWasAlreadyAddedIntoFridge,
    ProductWasSuccessfulAddedIntoProductsList,
    ScanFailed
}