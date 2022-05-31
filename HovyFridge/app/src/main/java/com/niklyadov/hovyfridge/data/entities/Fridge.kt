package com.niklyadov.hovyfridge.data.entities

import androidx.room.*
import com.google.gson.annotations.SerializedName
import com.niklyadov.hovyfridge.data.entities.relations.FridgeWithProducts

@Entity
data class Fridge (
    @PrimaryKey
    @SerializedName("id")
    val id : Int,

    @SerializedName("is_deleted")
    val isDeleted : Boolean = false,

    @ColumnInfo(name = "name")
    @SerializedName("name")
    var name : String,
)
{
    @Ignore
    @SerializedName("products")
    var products: List<Product> = listOf()
}