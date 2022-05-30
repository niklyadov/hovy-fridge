package com.niklyadov.hovyfridge.ui.fridge_details

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.viewModelScope
import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product
import com.niklyadov.hovyfridge.services.FridgesService
import com.niklyadov.hovyfridge.ui.base.BaseViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class DetailedFridgeViewModel @Inject constructor(
    private val fridgesService: FridgesService
): BaseViewModel() {
    private lateinit var _fridgeIdSharedViewModel : FridgeIdSharedViewModel
    private var _fridgeId : Int = 0
    val fridge : MutableLiveData<Fridge> = MutableLiveData()
    val loadInProgress : MutableLiveData<Boolean> = MutableLiveData()

    fun loadFridge() {
        viewModelScope.launch {
            loadInProgress.value = true;

            val getFridgeStatus = fridgesService.getFridge(_fridgeId).onFailure {
                error.value = it
            }

            fridge.apply {
                value = getFridgeStatus.getOrNull()
            }

            loadInProgress.value = false;
        }
    }

    fun renameFridge(fridgeName: String) {
        viewModelScope.launch {
            loadInProgress.value = true;

            fridgesService.renameFridge(_fridgeId, fridgeName).onFailure {
                error.value = it
            }
            loadFridge()

            loadInProgress.value = false;
        }
    }

    fun deleteFridge() {
        viewModelScope.launch {
            loadInProgress.value = true;

            fridgesService.deleteFridge(_fridgeId).onFailure {
                error.value = it
            }

            loadInProgress.value = false;
        }
    }

    fun addSharedViewModel (viewModel : FridgeIdSharedViewModel) {
        _fridgeIdSharedViewModel = viewModel

        if(_fridgeIdSharedViewModel.fridgeId != null) {
            _fridgeId = _fridgeIdSharedViewModel.fridgeId!!
        }
    }

    fun restoreProduct(item: Product) {
        viewModelScope.launch {
            loadInProgress.value = true;

            fridgesService.restoreProductInFridge(_fridgeId, item).onSuccess {
                loadFridge()
            }.onFailure {
                error.value = it
            }

            loadInProgress.value = false;
        }
    }

    fun deleteProduct(item: Product) {
        viewModelScope.launch {
            loadInProgress.value = true;

            fridgesService.removeProductFromFridge(_fridgeId, item).onSuccess {
                loadFridge()
            }.onFailure {
                error.value = it
            }

            loadInProgress.value = false;
        }
    }
}