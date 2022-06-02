package com.niklyadov.hovyfridge.presentation.ui.product_details.fridge

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class ProductDetailsFridgeSharedViewModel @Inject constructor() : ViewModel() {
    var fridgeId : Int = 0
    var productId : Int = 0
}