package com.niklyadov.hovyfridge.ui.products_list

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
class ProductsListViewModel  @Inject constructor(
    private val productsService : ProductsService,
    private val fridgesService: FridgesService
): BaseViewModel() {
    val products : MutableLiveData<MutableList<Product>> = MutableLiveData()

    fun updateProductsList(queryString: String? = null) {
        viewModelScope.launch {
            products.apply {
                value = productsService.getProductsList(queryString).onFailure {
                    error.value = it
                }.getOrNull()?.toMutableList()
            }
        }
    }

    fun deleteProduct(product : Product) {
        viewModelScope.launch {
            products.apply {
                value?.remove(product)
            }

            productsService.deleteProduct(product.id).onFailure {
                error.value = it
            }
        }
    }

    fun restoreProduct(product: Product, position : Int? = null) {
        viewModelScope.launch {
            products.apply {
                if(position != null) {
                    value?.add(position, product)
                } else {
                    value?.add(product)
                }
            }

            productsService.restoreProduct(product.id).onFailure {
                error.value = it
            }
        }
    }
}