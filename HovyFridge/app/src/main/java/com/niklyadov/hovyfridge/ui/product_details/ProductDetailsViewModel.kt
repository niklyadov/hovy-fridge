package com.niklyadov.hovyfridge.ui.product_details

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.viewModelScope
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.services.FridgesService
import com.niklyadov.hovyfridge.services.ProductsService
import com.niklyadov.hovyfridge.ui.base.BaseViewModel
import com.niklyadov.hovyfridge.ui.product_details.fridge.ProductDetailsFridgeViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class ProductDetailsViewModel @Inject constructor(
    private val productsService : ProductsService,
    private val fridgesService: FridgesService
) : BaseViewModel() {
    private lateinit var _sharedViewModel : ProductDetailsFridgeViewModel

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

    fun addSharedViewModel(sharedViewModel : ProductDetailsFridgeViewModel) {
        _sharedViewModel = sharedViewModel
    }

    fun deleteProduct() {
        viewModelScope.launch {
            if(product.value != null) {
                productsService.deleteProduct(product.value!!.id)
            }
        }
    }
}