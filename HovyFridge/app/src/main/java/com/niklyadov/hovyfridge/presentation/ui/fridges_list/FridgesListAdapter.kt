package com.niklyadov.hovyfridge.presentation.ui.fridges_list

import androidx.recyclerview.widget.RecyclerView
import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import com.niklyadov.hovyfridge.databinding.ItemFridgeBinding
import com.niklyadov.hovyfridge.data.entities.Fridge

class FridgesListAdapter() : ListAdapter<Fridge, FridgesListAdapter.ViewHolder>(IsDifferentCallback()) {

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
            ItemFridgeBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            ), mListener
        )
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val item = currentList[position]
        holder.fridgeName.text = item.name
        holder.fridgeDescription.text = "${item.productsAmount} products inside, id: ${item.id}"
    }

    override fun getItemCount(): Int = currentList.size

    inner class ViewHolder(binding: ItemFridgeBinding, listener: OnItemListeners) :
        RecyclerView.ViewHolder(binding.root) {
        val fridgeName: TextView = binding.fridgeName
        val fridgeDescription: TextView = binding.fridgeDescription

        override fun toString(): String {
            return super.toString() + " '" + fridgeName.text + "'"
        }

        init {
            itemView.setOnClickListener {
                listener.onClick(adapterPosition)
            }

            itemView.setOnLongClickListener {
                listener.onLongClick(adapterPosition)
            }
        }
    }

    class IsDifferentCallback : DiffUtil.ItemCallback<Fridge>() {
        override fun areItemsTheSame(oldItem: Fridge, newItem: Fridge): Boolean = oldItem.id == newItem.id
        override fun areContentsTheSame(oldItem: Fridge, newItem: Fridge): Boolean = oldItem.name == newItem.name
    }
}