package com.niklyadov.hovyfridge.presentation.ui.products_list

import android.os.Bundle
import android.view.*
import androidx.appcompat.widget.SearchView
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.snackbar.Snackbar
import com.niklyadov.hovyfridge.R
import com.niklyadov.hovyfridge.databinding.FragmentProductsListBinding
import com.niklyadov.hovyfridge.presentation.ui.base.BaseFragment
import com.niklyadov.hovyfridge.presentation.ui.product_details.ProductDetailsSharedViewModel
import com.niklyadov.hovyfridge.presentation.ui.scanner.ProductScanSharedViewModel
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class ProductsListFragment : BaseFragment() {

    private lateinit var _binding: FragmentProductsListBinding
    private val _viewModel : ProductsListViewModel by viewModels()
    private val _productScanSharedViewModel : ProductScanSharedViewModel by activityViewModels()
    private val _productDetailsSharedViewModel : ProductDetailsSharedViewModel by activityViewModels()

    private var _productListAdapter : ProductsListAdapter = ProductsListAdapter()


    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentProductsListBinding.inflate(inflater)

        setHasOptionsMenu(true)
        setViewModel(_viewModel)

        return _binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        recyclerViewSwipeHandle()

        _viewModel.loadInProgress.observe(viewLifecycleOwner) {
            _binding.progressBarLoading.visibility =
                if (it) View.VISIBLE else View.GONE
        }

        _productListAdapter.setOnClickListener(object : ProductsListAdapter.OnItemListeners {
            override fun onClick(position: Int) {
                val product = _productListAdapter.currentList[position]
                _productDetailsSharedViewModel.productId = product.id
                findNavController().navigate(R.id.action_navigation_products_list_to_navigation_product_details)
            }

            override fun onLongClick(position: Int): Boolean
                = true;
        })

        _binding.listOfProducts.adapter = _productListAdapter

        _viewModel.updateProductsList();

        _viewModel.products.observe(viewLifecycleOwner) {
            _productListAdapter.submitList(it)
        };
    }

    override fun onCreateOptionsMenu(menu: Menu, inflater: MenuInflater) {
        inflater.inflate(R.menu.products_list_actionbar, menu)
        val item = menu.findItem(R.id.products_list_search_item)
        val searchView = item.actionView as SearchView

        searchView.setOnQueryTextListener(object : SearchView.OnQueryTextListener {
            override fun onQueryTextSubmit(query: String?): Boolean {
                _viewModel.updateProductsList(query);
                return true;
            }

            override fun onQueryTextChange(newText: String?): Boolean {
                _viewModel.updateProductsList(newText);
                return true;
            }

        })

        super.onCreateOptionsMenu(menu, inflater)
    }

    override fun onOptionsItemSelected(item: MenuItem) = when (item.itemId) {
        R.id.products_list_add_product_item -> {
            _productScanSharedViewModel.fridgeId = 0
            findNavController().navigate(R.id.action_navigation_products_list_to_navigation_code_scanner)
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
                    val item = _productListAdapter.currentList[position]
                    _viewModel.deleteProduct(item)
                    _productListAdapter.notifyItemRemoved(position)

                    Snackbar.make(
                        _binding.root,
                        "${item.name} has been removed",
                        Snackbar.LENGTH_LONG
                    ).apply {
                        setAction("Cancel") {
                            _viewModel.restoreProduct(item, position)
                            _productListAdapter.notifyItemInserted(position)
                        }
                        show()
                    }
                }
            }

        }).attachToRecyclerView(_binding.listOfProducts)
    }
}