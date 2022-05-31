package com.niklyadov.hovyfridge.ui.fridges_list

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.viewModelScope
import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.services.FridgesService
import com.niklyadov.hovyfridge.ui.base.BaseViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class FridgesListViewModel @Inject constructor(
    private val fridgesService: FridgesService
)  : BaseViewModel() {
    val fridges : MutableLiveData<MutableList<Fridge>> = MutableLiveData()

    fun updateFridgesList() {
        viewModelScope.launch {
            fridges.apply {
                value = fridgesService.getFridges().onFailure {
                    error.value = it
                }.getOrNull()?.toMutableList()
            }
        }
    }

    fun addNewFridgeWithName(fridgeName : String) {
        viewModelScope.launch {
            fridgesService.addNewFridge(Fridge(0, false, fridgeName))
            updateFridgesList()
        }
    }
}