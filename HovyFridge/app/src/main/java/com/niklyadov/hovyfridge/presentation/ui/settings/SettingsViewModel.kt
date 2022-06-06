package com.niklyadov.hovyfridge.presentation.ui.settings

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.viewModelScope
import com.niklyadov.hovyfridge.services.UpdatesService
import com.niklyadov.hovyfridge.presentation.ui.base.BaseViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class SettingsViewModel @Inject constructor(
    private val updatesService: UpdatesService
) : BaseViewModel() {

    val checkUpdatesResponse : MutableLiveData<Boolean> = MutableLiveData()

    fun checkUpdates() {
        viewModelScope.launch {
            updatesService.downloadUpdate().onSuccess {
                checkUpdatesResponse.value = it
            }.onFailure {
                error.value = it
            }
        }
    }
}