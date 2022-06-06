package com.niklyadov.hovyfridge.presentation.ui.product_details.fridge

import android.app.AlertDialog
import android.os.Bundle
import android.view.*
import android.widget.SeekBar
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import com.niklyadov.hovyfridge.R
import com.niklyadov.hovyfridge.databinding.DialogProductEditBinding
import com.niklyadov.hovyfridge.databinding.FragmentProductDetailsFridgeBinding
import com.niklyadov.hovyfridge.presentation.ui.base.BaseFragment
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class ProductDetailsFridgeFragment : BaseFragment() {
    private lateinit var _binding : FragmentProductDetailsFridgeBinding
    private val _viewModel : ProductDetailsFridgeViewModel by viewModels()
    private val _productDetailsFridgeSharedViewModel : ProductDetailsFridgeSharedViewModel by activityViewModels()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentProductDetailsFridgeBinding.inflate(inflater)

        setHasOptionsMenu(true)
        setViewModel(_viewModel)

        return _binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        _viewModel.addSharedViewModel(_productDetailsFridgeSharedViewModel)

        _viewModel.product.observe(viewLifecycleOwner) {
            _binding.fridgeProductDetailsName.text = "${it.name} (id ${it.id})"
            _binding.fridgeProductDetailsBarcode.text =  it.barcode

            val productAmount = it.amount.coerceAtMost(100).toInt()
            _binding.amountOfProductSlider.progress = productAmount
            _binding.amountOfProductSliderValue.text = "$productAmount %"

            //_binding.fridgeProductDetailsAddedDatetime.text = it.createdDateTime.toString()
        }

        _binding.amountOfProductSlider.setOnSeekBarChangeListener(object : SeekBar.OnSeekBarChangeListener {
            override fun onStopTrackingTouch(sb: SeekBar) {
                _viewModel.changeProductAmount(sb.progress)
            }
            override fun onProgressChanged(sb: SeekBar, value: Int, p2: Boolean) {
                _binding.amountOfProductSliderValue.text = "$value %"
            }
            override fun onStartTrackingTouch(sb: SeekBar) {}
        })

        _viewModel.loadProductInfo(_productDetailsFridgeSharedViewModel.productId)
    }

    override fun onCreateOptionsMenu(menu: Menu, inflater: MenuInflater) {
        inflater.inflate(R.menu.product_details_actionbar, menu)
        super.onCreateOptionsMenu(menu, inflater)
    }

    override fun onOptionsItemSelected(item: MenuItem) = when (item.itemId) {
        R.id.product_details_delete -> {

            _viewModel.deleteProductFromFridge()
            findNavController().popBackStack()

            true
        }
        R.id.product_details_edit -> {
            showEditProductDialog()
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