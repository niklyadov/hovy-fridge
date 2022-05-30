package com.niklyadov.hovyfridge.data.entities

import androidx.room.*
import com.google.gson.annotations.SerializedName
import com.niklyadov.hovyfridge.data.entities.relations.FridgeWithProducts

@Entity
data class Fridge (
    @PrimaryKey
    @SerializedName("id")
    val id : Int,

    @ColumnInfo(name = "name")
    @SerializedName("name")
    var name : String,
)
{
    @Ignore
    @SerializedName("products")
    var products: List<Product> = listOf()
}