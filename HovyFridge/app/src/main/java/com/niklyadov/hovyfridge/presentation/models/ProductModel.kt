package com.niklyadov.hovyfridge.presentation.models

import android.annotation.SuppressLint
import com.niklyadov.hovyfridge.data.entities.Product
import java.time.Instant
import java.time.LocalDateTime
import java.time.ZoneOffset
import java.util.*

data class ProductModel(
    var id : Int,
    private var isDeleted: Boolean,
    private var fridgeId : Int?,
    var barcode : String,
    var name : String,
    var amount : Short,
    var createdDateTime: LocalDateTime?
) {

    @SuppressLint("NewApi")
    fun toEntity(): Product {
        val t = createdDateTime?.atZone(ZoneOffset.UTC)?.toEpochSecond()
        return Product(id, isDeleted, fridgeId, barcode, name, amount, t)
    }

    companion object {
        @SuppressLint("NewApi")
        fun fromEntity(entity : Product) : ProductModel {

            var t : LocalDateTime? = null

            entity.createdTimestamp?.let {
                t = LocalDateTime.ofInstant(
                    Instant.ofEpochSecond(it),
                    TimeZone.getDefault().toZoneId()
                )
            }

            return ProductModel(entity.id, entity.isDeleted, entity.fridgeId, entity.barcode, entity.name,entity.amount, t?:null)
        }
    }
}