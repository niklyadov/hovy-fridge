package com.niklyadov.hovyfridge.presentation.ui.fridge_details

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject


@HiltViewModel
class FridgeIdSharedViewModel @Inject constructor() : ViewModel() {
    var fridgeId : Int? = 1
}