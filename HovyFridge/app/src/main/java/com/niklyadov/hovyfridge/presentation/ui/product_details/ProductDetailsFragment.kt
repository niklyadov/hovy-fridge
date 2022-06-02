package com.niklyadov.hovyfridge.presentation.ui.product_details

import android.app.AlertDialog
import android.os.Bundle
import android.view.*
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import com.niklyadov.hovyfridge.R
import com.niklyadov.hovyfridge.databinding.DialogProductEditBinding
import com.niklyadov.hovyfridge.databinding.FragmentProductDetailsBinding
import com.niklyadov.hovyfridge.presentation.ui.base.BaseFragment
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class ProductDetailsFragment : BaseFragment() {
    private lateinit var _binding : FragmentProductDetailsBinding
    private val _viewModel : ProductDetailsViewModel by viewModels()
    private val _productDetailsSharedViewModel : ProductDetailsSharedViewModel by activityViewModels()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentProductDetailsBinding.inflate(inflater)

        setHasOptionsMenu(true)
        setViewModel(_viewModel)

        return _binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        _viewModel.product.observe(viewLifecycleOwner) {
            _binding.productDetailsName.text = "${it.name} (id ${it.id})"
            _binding.productDetailsBarcode.text =  it.barcode
        }

        _viewModel.loadProductInfo(_productDetailsSharedViewModel.productId)

    }

    override fun onCreateOptionsMenu(menu: Menu, inflater: MenuInflater) {
        inflater.inflate(R.menu.product_details_actionbar, menu)
        super.onCreateOptionsMenu(menu, inflater)
    }

    override fun onOptionsItemSelected(item: MenuItem) = when (item.itemId) {
        R.id.product_details_delete -> {

            _viewModel.deleteProduct()
            findNavController().popBackStack()

            true
        }
        R.id.product_details_edit -> {
            showEditProductDialog();
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

    private fun showEditProductDialog() {
        val product = _viewModel.product.value ?: return
        val dialogAddProductBinding = DialogProductEditBinding.inflate(requireActivity().layoutInflater)
        dialogAddProductBinding.productEditDialogProductName.setText(product.name)

        val alertDialog: AlertDialog = activity.let {
            val builder = AlertDialog.Builder(it)
            builder.apply {
                setView(dialogAddProductBinding.root)
                setPositiveButton("Ok") { dialog, id ->
                    val productName = dialogAddProductBinding.productEditDialogProductName.text.toString()
                    if(productName.isNotBlank() && productName.isNotEmpty()) {
                        _viewModel.renameProduct(productName)
                    }
                }
                setNegativeButton("Cancel") {
                        dialog, id -> dialog.dismiss()
                }
            }
            builder.create()
        }

        alertDialog.show()
    }
}