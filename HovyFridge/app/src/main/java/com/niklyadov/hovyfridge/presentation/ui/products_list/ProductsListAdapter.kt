package com.niklyadov.hovyfridge.presentation.ui.products_list

import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import com.niklyadov.hovyfridge.databinding.ItemProductBinding
import com.niklyadov.hovyfridge.data.entities.Product

class ProductsListAdapter  : ListAdapter<Product, ProductsListAdapter.ViewHolder>(IsDifferentCallback()) {

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
            ItemProductBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            ), mListener
        )
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val item = currentList[position]
        holder.productName.text = item.name
        holder.productDescription.text = "id: ${item.id}, barcode: ${item.barcode}"
    }

    override fun getItemCount(): Int = currentList.size

    inner class ViewHolder(binding: ItemProductBinding, listener: OnItemListeners) :
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
        override fun areItemsTheSame(oldItem: Product, newItem: Product): Boolean = oldItem.barcode == newItem.barcode
        override fun areContentsTheSame(oldItem: Product, newItem: Product): Boolean = oldItem.name == newItem.name
    }
}