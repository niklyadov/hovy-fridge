package com.niklyadov.hovyfridge.presentation.ui.fridge_details

import android.app.AlertDialog
import android.os.Bundle
import android.view.*
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.snackbar.Snackbar
import com.niklyadov.hovyfridge.R
import com.niklyadov.hovyfridge.databinding.DetailedFridgeFragmentBinding
import com.niklyadov.hovyfridge.databinding.DialogFridgeEditBinding
import com.niklyadov.hovyfridge.presentation.ui.base.BaseFragment
import com.niklyadov.hovyfridge.presentation.ui.product_details.ProductDetailsSharedViewModel
import com.niklyadov.hovyfridge.presentation.ui.product_details.fridge.ProductDetailsFridgeSharedViewModel
import com.niklyadov.hovyfridge.presentation.ui.scanner.ProductScanSharedViewModel
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class DetailedFridgeFragment : BaseFragment() {
    private lateinit var _binding: DetailedFridgeFragmentBinding

    private val _viewModel : DetailedFridgeViewModel by viewModels()
    private val _fridgeIdSharedViewModel : FridgeIdSharedViewModel by activityViewModels()
    private val _productScanSharedViewModel : ProductScanSharedViewModel by activityViewModels()
    private val _productDetailsSharedViewModel : ProductDetailsSharedViewModel by activityViewModels()
    private val _productDetailsFridgeSharedViewModel : ProductDetailsFridgeSharedViewModel by activityViewModels()

    private val _fridgeProductListAdapter : FridgeProductListAdapter = FridgeProductListAdapter()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = DetailedFridgeFragmentBinding.inflate(inflater)

        setHasOptionsMenu(true)
        setViewModel(_viewModel)

        return _binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        recyclerViewSwipeHandle()

        _fridgeProductListAdapter.setOnClickListener(object : FridgeProductListAdapter.OnItemListeners{
            override fun onClick(position: Int) {
                val product = _fridgeProductListAdapter.currentList[position]
                if (_viewModel.fridge.value != null) {
                    _productDetailsFridgeSharedViewModel.fridgeId = _viewModel.fridge.value!!.id
                }
                _productDetailsFridgeSharedViewModel.productId = product.id
                findNavController().navigate(R.id.action_navigation_fridge_detailed_to_navigation_product_details_fridge)
            }

            override fun onLongClick(position: Int): Boolean
                = true
        })

        _binding.listOfProductsInFridge.adapter = _fridgeProductListAdapter

        _viewModel.loadInProgress.observe(viewLifecycleOwner) {
            _binding.detailedFridgeFragmentProgressBarLoading.visibility =
                if (it) View.VISIBLE else View.GONE
        }

        _viewModel.fridge.observe(viewLifecycleOwner) {
            _binding.detailedFridgeFragmentFridgeName.text = it.name
            _fridgeProductListAdapter.submitList(it.products)
        };

        _viewModel.addSharedViewModel(_fridgeIdSharedViewModel)
        _viewModel.loadFridge();
    }

    override fun onCreateOptionsMenu(menu: Menu, inflater: MenuInflater) {
        inflater.inflate(R.menu.fridge_details_actionbar, menu)
        super.onCreateOptionsMenu(menu, inflater)
    }

    override fun onOptionsItemSelected(item: MenuItem) = when (item.itemId) {
        R.id.fridge_details_action_bar_add_product_item -> {
            _productScanSharedViewModel.fridgeId = _fridgeIdSharedViewModel.fridgeId
            findNavController().navigate(R.id.action_navigation_fridge_detailed_to_navigation_code_scanner)
            true
        }
        R.id.fridge_details_action_bar_edit_fridge_item -> {
            showEditFridgeDialog();
            true
        }
        R.id.fridge_details_action_bar_delete_fridge_item -> {
            _viewModel.deleteFridge();
            findNavController().popBackStack()
            true;
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

    private fun recyclerViewSwipeHandle () {
        ItemTouchHelper(object : ItemTouchHelper.SimpleCallback(
            ItemTouchHelper.UP or ItemTouchHelper.DOWN,
            ItemTouchHelper.LEFT /* or ItemTouchHelper.RIGHT*/
        ) {
            override fun onMove(
                recyclerView: RecyclerView,
                viewHolder: RecyclerView.ViewHolder,
                target: RecyclerView.ViewHolder
            ): Boolean = true

            override fun onSwiped(viewHolder: RecyclerView.ViewHolder, direction: Int) {
                if (direction == ItemTouchHelper.LEFT) {
                    val position = viewHolder.adapterPosition
                    val item = _fridgeProductListAdapter.currentList[position]
                    _viewModel.deleteProduct(item)
                    _fridgeProductListAdapter.notifyItemRemoved(position)

                    Snackbar.make(
                        _binding.root,
                        "${item.name} has been removed",
                        Snackbar.LENGTH_LONG
                    ).apply {
                        setAction("Cancel") {
                            _viewModel.restoreProduct(item)
                            _fridgeProductListAdapter.notifyItemInserted(position)
                        }
                        show()
                    }
                }
            }

        }).attachToRecyclerView(_binding.listOfProductsInFridge)
    }

    private fun showEditFridgeDialog() {
        val fridge = _viewModel.fridge.value ?: return
        val dialogAddProductBinding = DialogFridgeEditBinding.inflate(requireActivity().layoutInflater)
        dialogAddProductBinding.fridgeEditDialogFridgeName.setText(fridge.name)

        val alertDialog: AlertDialog = activity.let {
            val builder = AlertDialog.Builder(it)
            builder.apply {
                setView(dialogAddProductBinding.root)
                setPositiveButton("Ok") { dialog, id ->
                    val fridgeName = dialogAddProductBinding.fridgeEditDialogFridgeName.text.toString()
                    if(fridgeName.isNotBlank() && fridgeName.isNotEmpty()) {
                        _viewModel.renameFridge(fridgeName)
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