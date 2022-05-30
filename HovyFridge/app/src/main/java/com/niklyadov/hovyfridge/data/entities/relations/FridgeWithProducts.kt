package com.niklyadov.hovyfridge.data.entities.relations

import androidx.room.Embedded
import androidx.room.Relation
import com.niklyadov.hovyfridge.data.entities.Fridge
import com.niklyadov.hovyfridge.data.entities.Product

data class FridgeWithProducts (
    @Embedded
    val fridge: Fridge,

    @Relation(entity = Product::class, parentColumn = "id", entityColumn = "fridge_id")
    val products: List<Product>
)

fun FridgeWithProducts.toFridge() : Fridge {
    fridge.products = this.products
    return fridge
}