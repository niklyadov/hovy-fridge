package com.niklyadov.hovyfridge.ui.product_details

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class ProductDetailsSharedViewModel @Inject constructor() : ViewModel() {
    var productId : Int = 0
}