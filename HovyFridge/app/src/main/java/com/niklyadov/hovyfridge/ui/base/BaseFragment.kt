package com.niklyadov.hovyfridge.ui.base

import androidx.appcompat.app.AlertDialog
import androidx.fragment.app.Fragment
import androidx.preference.PreferenceManager

open class BaseFragment: Fragment() {

    fun setViewModel (viewModel : BaseViewModel) {
        val sp = PreferenceManager.getDefaultSharedPreferences(context)
        val isDialogErrorEnabled = sp.getBoolean("show_errors", true)
        if (isDialogErrorEnabled) {
            viewModel.error.observe(viewLifecycleOwner) {
                showErrorDialog("---= cause =---\n${it.cause?:""} \n---= message =---\n ${it.message} \n---= stack =---\n ${it.stackTraceToString()}")
            }
        }
    }

    private fun showErrorDialog(errorMessage: String) {
        if (activity == null) return;

        val builder = AlertDialog.Builder(requireActivity())
        builder.setTitle("Some error.")
        builder.setMessage(errorMessage)
        builder.show()
    }

}