package com.niklyadov.hovyfridge.ui.product_details.fridge

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.viewModelScope
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.services.FridgesService
import com.niklyadov.hovyfridge.services.ProductsService
import com.niklyadov.hovyfridge.ui.base.BaseViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class ProductDetailsFridgeViewModel @Inject constructor(
    private val productsService : ProductsService,
    private val fridgesService: FridgesService
) : BaseViewModel() {
    private lateinit var _sharedViewModel : ProductDetailsFridgeSharedViewModel

    var product : MutableLiveData<Product> = MutableLiveData()

    fun loadProductInfo(productId : Int) {
        viewModelScope.launch {
            product.apply {
                value = productsService.getProduct(productId).onFailure {
                    error.value = it
                }.getOrNull()
            }
        }
    }

    fun deleteProductFromFridge() {
        viewModelScope.launch {
            if(product.value != null) {
                fridgesService.removeProductFromFridge(_sharedViewModel.fridgeId, product.value!!)
            }
        }
    }

    fun addSharedViewModel(sharedViewModel : ProductDetailsFridgeSharedViewModel ) {
        _sharedViewModel = sharedViewModel
    }
}