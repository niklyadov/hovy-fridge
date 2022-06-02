package com.niklyadov.hovyfridge.presentation.ui.fridges_list

import android.app.AlertDialog
import android.os.Bundle
import android.view.*
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import com.niklyadov.hovyfridge.R
import com.niklyadov.hovyfridge.databinding.DialogFidgeAddBinding
import com.niklyadov.hovyfridge.databinding.FridgesListFragmentBinding
import com.niklyadov.hovyfridge.presentation.ui.base.BaseFragment
import com.niklyadov.hovyfridge.presentation.ui.fridge_details.FridgeIdSharedViewModel
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class FridgesListFragment : BaseFragment() {
    private lateinit var _binding: FridgesListFragmentBinding

    private val _viewModel : FridgesListViewModel by viewModels()
    private val _fridgeIdSharedVM : FridgeIdSharedViewModel by activityViewModels()

    private val _fridgesListAdapter: FridgesListAdapter = FridgesListAdapter()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FridgesListFragmentBinding.inflate(inflater)

        setHasOptionsMenu(true)
        setViewModel(_viewModel)

        return _binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        _viewModel.loadInProgress.observe(viewLifecycleOwner) {
            _binding.progressBarLoading.visibility =
                if (it) View.VISIBLE else View.GONE
        }

        _fridgesListAdapter.setOnClickListener(object : FridgesListAdapter.OnItemListeners {
            override fun onClick(position: Int) {
                val fridge = _fridgesListAdapter.currentList[position]
                _fridgeIdSharedVM.fridgeId = fridge.id
                findNavController().navigate(R.id.action_navigation_fridges_list_to_navigation_fridge_detailed)
            }

            override fun onLongClick(position: Int): Boolean {

//                // delete
//                homeViewModel.fridgesList.value = homeViewModel.fridgesList.value?.toMutableList().apply {
//                    this?.removeAt(position)
//                }?.toMutableList()

                return true;
            }
        })

        _binding.listOfFridges.adapter = _fridgesListAdapter

        _viewModel.fridges.observe(viewLifecycleOwner) {
            _fridgesListAdapter.submitList(it)
        }

        _viewModel.updateFridgesList()
    }

    override fun onCreateOptionsMenu(menu: Menu, inflater: MenuInflater) {
        inflater.inflate(R.menu.fridges_list_actionbar, menu)
        super.onCreateOptionsMenu(menu, inflater)
    }

    override fun onOptionsItemSelected(item: MenuItem) = when (item.itemId) {
        R.id.fridges_list_add_item -> {
            showNewFridgeDialog()
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

    private fun showNewFridgeDialog() {
        val dialogAddProductBinding  = DialogFidgeAddBinding.inflate(requireActivity().layoutInflater)
        val alertDialog: AlertDialog = activity.let {
            val builder = AlertDialog.Builder(it)
            builder.apply {
                setView(dialogAddProductBinding.root)
                setPositiveButton("Ok") { dialog, id ->
                    val fridgeName = dialogAddProductBinding.fridgeAddDialogFridgeName.text.toString()
                    if(fridgeName.isNotBlank() && fridgeName.isNotEmpty()) {
                        _viewModel.addNewFridgeWithName(fridgeName)
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