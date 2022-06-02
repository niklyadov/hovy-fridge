package com.niklyadov.hovyfridge.presentation.ui.scanner

import android.Manifest
import android.app.AlertDialog
import android.os.Bundle
import android.view.LayoutInflater
import android.view.MenuItem
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.lifecycle.Observer
import androidx.navigation.fragment.findNavController
import com.budiyev.android.codescanner.CodeScanner
import com.budiyev.android.codescanner.DecodeCallback
import com.niklyadov.hovyfridge.databinding.CodeScannerFragmentBinding
import com.niklyadov.hovyfridge.databinding.DialogAddProductBinding
import com.niklyadov.hovyfridge.enums.BarcodeScanResult
import com.niklyadov.hovyfridge.presentation.ui.base.BaseFragment
import com.tbruyelle.rxpermissions2.RxPermissions
import dagger.hilt.android.AndroidEntryPoint


@AndroidEntryPoint
class ProductScanFragment : BaseFragment() {
    private val _productScanViewModel: ProductScanViewModel by viewModels()
    private val _productScanSharedViewModel : ProductScanSharedViewModel by activityViewModels()

    private lateinit var _binding: CodeScannerFragmentBinding
    private lateinit var _codeScanner: CodeScanner

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = CodeScannerFragmentBinding.inflate(inflater)

        setHasOptionsMenu(true)
        setViewModel(_productScanViewModel)
        _productScanViewModel.addSharedViewModel(_productScanSharedViewModel)

        return _binding.root
    }


    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        val scannerView = _binding.scannerView
        val activity = requireActivity()

        _productScanViewModel.scanStatusResult.observe(viewLifecycleOwner, Observer {
            _binding.loadingProgress.visibility = View.GONE
            when (it) {
                BarcodeScanResult.ScanFailed -> {
                    Toast.makeText(activity, "Some error", Toast.LENGTH_LONG).show()
                    findNavController().popBackStack()
                }
                BarcodeScanResult.ProductWasFound -> {
                    Toast.makeText(activity, "Product '${_productScanViewModel.lastScannedProduct.value?.name}' already added in products list", Toast.LENGTH_LONG).show()
                    findNavController().popBackStack()
                }
                BarcodeScanResult.ProductWasSuccessfulAddedIntoProductsList -> {
                    Toast.makeText(activity, "Product successful added in products list", Toast.LENGTH_LONG).show()
                    findNavController().popBackStack()
                }
                BarcodeScanResult.ProductWasSuccessfulAddedIntoFridge -> {
                    Toast.makeText(activity, "Product '${_productScanViewModel.lastScannedProduct.value?.name}' added into your fridge", Toast.LENGTH_LONG).show()
                    findNavController().popBackStack()
                }
                BarcodeScanResult.ProductWasAlreadyAddedIntoFridge -> {
                    Toast.makeText(activity, "Product '${_productScanViewModel.lastScannedProduct.value?.name}' already added in the fridge", Toast.LENGTH_LONG).show()
                    findNavController().popBackStack()
                }
                BarcodeScanResult.ProductWasNotFound -> {
                    showAddNewProductDialog()
                }
            }
        });

        RxPermissions(activity).request(Manifest.permission.CAMERA)
            .subscribe { granted: Boolean ->
                if (granted) {

                    _codeScanner = CodeScanner(activity, scannerView)
                    _codeScanner.decodeCallback = DecodeCallback {
                        activity.runOnUiThread {
                            _binding.loadingProgress.visibility = View.VISIBLE
                            _productScanViewModel.onCodeScanned(it.text);
                        }
                    }
                    scannerView.setOnClickListener {
                        _codeScanner.startPreview()
                    }

                } else {
                    Toast.makeText(activity, "Cancelled", Toast.LENGTH_LONG).show()
                    findNavController().popBackStack()
                }
            }
    }

    private fun showAddNewProductDialog() {
        val dialogAddProductBinding  = DialogAddProductBinding.inflate(requireActivity().layoutInflater)

        val alertDialog: AlertDialog = activity.let {
            val builder = AlertDialog.Builder(it)
            builder.apply {
                setView(dialogAddProductBinding.root)
                setPositiveButton("Ok") { dialog, id ->
                    val productName = dialogAddProductBinding.etUserInput.text.toString()

                    if(productName.isNotBlank() && productName.isNotEmpty()) {
                        _binding.loadingProgress.visibility = View.VISIBLE

                        _productScanViewModel.onNewProductCodeScanned(productName)
                    } else {
                        Toast.makeText(activity, "Cancelled", Toast.LENGTH_LONG).show()
                        findNavController().popBackStack()
                        dialog.dismiss()
                    }
                }
                setNegativeButton("Cancel") {
                        dialog, id ->
                    Toast.makeText(activity, "Cancelled", Toast.LENGTH_LONG).show()
                    findNavController().popBackStack()
                    dialog.dismiss()
                }
            }
            builder.create()
        }

        alertDialog.show()
    }

    override fun onResume() {
        super.onResume()
        _codeScanner.startPreview()
    }

    override fun onPause() {
        _codeScanner.releaseResources()
        super.onPause()
    }

    override fun onOptionsItemSelected(item: MenuItem) = when (item.itemId) {
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