package com.niklyadov.hovyfridge.ui.scanner

import android.util.Log
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.viewModelScope
import com.niklyadov.hovyfridge.enums.BarcodeScanResult
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.services.FridgesService
import com.niklyadov.hovyfridge.services.ProductsService
import com.niklyadov.hovyfridge.ui.base.BaseViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class ProductScanViewModel @Inject constructor(
    private val productsService : ProductsService,
    private val fridgesService: FridgesService
) : BaseViewModel()  {

    private lateinit var _productScanSharedViewModel : ProductScanSharedViewModel
    private var _fridgeId : Int = 0

    val scanStatusResult : MutableLiveData<BarcodeScanResult> = MutableLiveData()
    val lastScannedCode :  MutableLiveData<String> = MutableLiveData()
    val lastScannedProduct: MutableLiveData<Product> = MutableLiveData()

    fun onCodeScanned(code : String) {
        viewModelScope.launch{
            lastScannedCode.value = code

            val addToFridge = _fridgeId != 0

            val product = productsService.getProductByBarcode(code).getOrNull()

            if(product == null) {
                scanStatusResult.value = BarcodeScanResult.ProductWasNotFound
                return@launch;
            } else {
                lastScannedProduct.value = product
            }

            if(addToFridge) {
                if(fridgesService.isFridgeContainsProductWithBarcode(_fridgeId, code)) {

                    scanStatusResult.value = BarcodeScanResult.ProductWasAlreadyAddedIntoFridge

                } else {
                    scanStatusResult.value = BarcodeScanResult.ProductWasSuccessfulAddedIntoFridge
                    fridgesService.putProductIntoFridge(_fridgeId, product).onFailure {
                        error.value = it
                    }
                }

                return@launch;
            }

            scanStatusResult.value = BarcodeScanResult.ProductWasFound
        }
    }

    fun onNewProductCodeScanned(productName: String) {
        viewModelScope.launch {
            val code = lastScannedCode.value ?: return@launch;
            val addToFridge = _fridgeId != 0
            val product = productsService.addProductToList(Product(0,false,0, code, productName)).onFailure {
                error.value = it
            }.getOrNull()

            if(product == null) {
                scanStatusResult.value = BarcodeScanResult.ScanFailed
                return@launch
            }

            lastScannedProduct.value = product

            if(addToFridge) {
                fridgesService.putProductIntoFridge(_fridgeId, product).onFailure {
                    error.value = it
                }
                scanStatusResult.value = BarcodeScanResult.ProductWasSuccessfulAddedIntoFridge
                return@launch
            }

            scanStatusResult.value = BarcodeScanResult.ProductWasSuccessfulAddedIntoProductsList
        }
    }

    fun addSharedViewModel (viewModel : ProductScanSharedViewModel) {
        _productScanSharedViewModel = viewModel

        if(_productScanSharedViewModel.fridgeId != null) {
            _fridgeId = _productScanSharedViewModel.fridgeId!!
        }
    }
}