package com.niklyadov.hovyfridge.data.entities

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import com.google.gson.annotations.SerializedName

@Entity
data class Product(
    @PrimaryKey(autoGenerate = false)
    @SerializedName("id")
    val id : Int,

    @SerializedName("is_deleted")
    val isDeleted : Boolean = false,

    @ColumnInfo(name = "fridge_id")
    @SerializedName("fridgeId")
    var fridgeId : Int,

    @ColumnInfo(name = "bar_code")
    @SerializedName("barCode")
    val barcode : String,

    @ColumnInfo(name = "name")
    @SerializedName("name")
    var name : String
)