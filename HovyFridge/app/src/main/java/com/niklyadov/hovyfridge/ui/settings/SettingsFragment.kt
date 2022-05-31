package com.niklyadov.hovyfridge.ui.settings

import android.Manifest
import android.os.Bundle
import android.view.*
import android.widget.Toast
import androidx.appcompat.app.AppCompatDelegate
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import androidx.preference.PreferenceFragmentCompat
import androidx.preference.PreferenceManager
import com.niklyadov.hovyfridge.R
import com.niklyadov.hovyfridge.databinding.FragmentSettingsBinding
import com.niklyadov.hovyfridge.ui.base.BaseFragment
import com.tbruyelle.rxpermissions2.RxPermissions
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class SettingsFragment : PreferenceFragmentCompat() {

    private val _viewModel : SettingsViewModel by viewModels()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        setHasOptionsMenu(true);

        return super.onCreateView(inflater, container, savedInstanceState)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        _viewModel.checkUpdatesResponse.observe(viewLifecycleOwner) {
            Toast.makeText(requireContext(), if(it) "Starting downloading update..." else "Your version is latest!", Toast.LENGTH_SHORT).show()
        }
        _viewModel.error.observe(viewLifecycleOwner) {
            Toast.makeText(requireContext(), "Some error while downloading updates", Toast.LENGTH_SHORT).show()
        }
    }

    override fun onCreatePreferences(savedInstanceState: Bundle?, rootKey: String?) {
        setPreferencesFromResource(R.xml.root_preferences, rootKey)

        val sp = PreferenceManager.getDefaultSharedPreferences(activity)
        sp.registerOnSharedPreferenceChangeListener { _, _ ->
            AppCompatDelegate.setDefaultNightMode(
                if (sp.getBoolean("use_dark_theme", false)) AppCompatDelegate.MODE_NIGHT_YES else AppCompatDelegate.MODE_NIGHT_NO
            )
        }
    }

    override fun onCreateOptionsMenu(menu: Menu, inflater: MenuInflater) {
        inflater.inflate(R.menu.settings_actionbar, menu)
        super.onCreateOptionsMenu(menu, inflater)
    }

    override fun onOptionsItemSelected(item: MenuItem) = when (item.itemId) {
        R.id.setting_download_last_update -> {
            RxPermissions(requireActivity()).request(Manifest.permission.WRITE_EXTERNAL_STORAGE)
                .subscribe { granted: Boolean ->
                    if (granted) _viewModel.checkUpdates()
                    else {
                        Toast.makeText(requireActivity(), "Please, provide the permission",Toast.LENGTH_SHORT).show();
                    }
                }
            true
        }
        android.R.id.home -> {
            findNavController().popBackStack()
            true
        }
        else -> {
            // If we got here, the user's action was not recognized.
            // Invoke the superclass to handle it.
            super.onOptionsItemSelected(item)
        }
    }
}