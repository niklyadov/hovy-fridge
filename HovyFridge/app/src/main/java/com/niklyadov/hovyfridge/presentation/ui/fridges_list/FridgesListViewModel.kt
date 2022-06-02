package com.niklyadov.hovyfridge.presentation.ui.fridges_list

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.viewModelScope
import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.services.FridgesService
import com.niklyadov.hovyfridge.presentation.ui.base.BaseViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class FridgesListViewModel @Inject constructor(
    private val fridgesService: FridgesService
)  : BaseViewModel() {
    val fridges : MutableLiveData<MutableList<Fridge>> = MutableLiveData()
    val loadInProgress: MutableLiveData<Boolean> = MutableLiveData()

    fun updateFridgesList() {
        loadInProgress.value = true
        viewModelScope.launch {
            fridges.apply {
                value = fridgesService.getFridges().onFailure {
                    error.value = it
                }.getOrNull()?.toMutableList()
            }
            loadInProgress.value = false
        }
    }

    fun addNewFridgeWithName(fridgeName : String) {
        viewModelScope.launch {
            fridgesService.addNewFridge(Fridge(0, false, fridgeName))
            updateFridgesList()
        }
    }
}