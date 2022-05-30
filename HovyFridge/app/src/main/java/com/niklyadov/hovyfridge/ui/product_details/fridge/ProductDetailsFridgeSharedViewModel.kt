package com.niklyadov.hovyfridge.ui.product_details.fridge

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class ProductDetailsFridgeSharedViewModel @Inject constructor() : ViewModel() {
    val fridgeId : Int = 0
    var productId : Int = 0
}