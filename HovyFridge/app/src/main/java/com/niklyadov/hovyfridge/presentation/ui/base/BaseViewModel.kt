package com.niklyadov.hovyfridge.presentation.ui.base

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

open class BaseViewModel : ViewModel() {
    val error : MutableLiveData<Throwable> = MutableLiveData()
}