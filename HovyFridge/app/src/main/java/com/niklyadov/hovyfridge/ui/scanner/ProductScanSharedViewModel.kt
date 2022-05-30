package com.niklyadov.hovyfridge.ui.scanner

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject


@HiltViewModel
class ProductScanSharedViewModel @Inject constructor() : ViewModel() {
    var fridgeId : Int? = null
}