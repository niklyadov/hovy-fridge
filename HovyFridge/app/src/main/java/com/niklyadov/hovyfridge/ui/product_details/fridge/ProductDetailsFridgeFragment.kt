package com.niklyadov.hovyfridge.ui.product_details.fridge

import android.os.Bundle
import android.view.*
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import com.niklyadov.hovyfridge.R
import com.niklyadov.hovyfridge.databinding.FragmentProductDetailsBinding
import com.niklyadov.hovyfridge.ui.base.BaseFragment
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class ProductDetailsFridgeFragment : BaseFragment() {
    private lateinit var _binding : FragmentProductDetailsBinding
    private val _viewModel : ProductDetailsFridgeViewModel by viewModels()
    private val _productDetailsSharedViewModel : ProductDetailsFridgeSharedViewModel by activityViewModels()

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

        _viewModel.addSharedViewModel(_productDetailsSharedViewModel)

        _viewModel.product.observe(viewLifecycleOwner) {
            _binding.productDetails.text = "${it.name} ${it.id} ${it.barcode}"
        }

        _viewModel.loadProductInfo(_productDetailsSharedViewModel.productId)
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