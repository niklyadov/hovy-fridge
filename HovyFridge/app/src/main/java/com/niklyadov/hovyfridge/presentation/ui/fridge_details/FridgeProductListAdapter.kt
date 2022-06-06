package com.niklyadov.hovyfridge.presentation.ui.fridge_details

import androidx.recyclerview.widget.RecyclerView
import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import com.niklyadov.hovyfridge.databinding.ItemFridgeProductBinding
import com.niklyadov.hovyfridge.data.entities.Product

class FridgeProductListAdapter() : ListAdapter<Product, FridgeProductListAdapter.ViewHolder>(IsDifferentCallback()) {

    private lateinit var mListener : OnItemListeners

    interface OnItemListeners {
        fun onClick(position: Int)
        fun onLongClick(position: Int) : Boolean
    }

    fun setOnClickListener (listener : OnItemListeners) {
        mListener = listener
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {

        return ViewHolder(
            ItemFridgeProductBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            ), mListener
        )
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val item = currentList[position]
        holder.productName.text = item.name
        holder.productDescription.text = "${item.amount} % | barcode ${item.barcode}, id ${item.id}"
    }

    override fun getItemCount(): Int = currentList.size

    inner class ViewHolder(binding: ItemFridgeProductBinding, listener: OnItemListeners) :
        RecyclerView.ViewHolder(binding.root) {
        val productName: TextView = binding.productName
        val productDescription: TextView = binding.productDescription

        init {
            itemView.setOnClickListener {
                listener.onClick(adapterPosition)
            }

            itemView.setOnLongClickListener {
                listener.onLongClick(adapterPosition)
            }
        }
    }

    class IsDifferentCallback : DiffUtil.ItemCallback<Product>() {
        override fun areItemsTheSame(oldItem: Product, newItem: Product): Boolean = oldItem.id == newItem.id
        override fun areContentsTheSame(oldItem: Product, newItem: Product): Boolean = oldItem.barcode == newItem.barcode
    }
}