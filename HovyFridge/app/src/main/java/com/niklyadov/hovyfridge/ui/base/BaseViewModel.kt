package com.niklyadov.hovyfridge.ui.base

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

open class BaseViewModel : ViewModel() {
    val error : MutableLiveData<Throwable> = MutableLiveData()
}