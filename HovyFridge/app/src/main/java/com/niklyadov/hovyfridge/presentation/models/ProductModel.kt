package com.niklyadov.hovyfridge.presentation.models

import android.annotation.SuppressLint
import com.niklyadov.hovyfridge.data.entities.Product
//import kotlinx.datetime.LocalDateTime
//import kotlinx.datetime.TimeZone

data class ProductModel(
    var id : Int,
    private var isDeleted: Boolean,
    private var fridgeId : Int?,
    var barcode : String,
    var name : String,
    var amount : Short,
    //var createdDateTime: LocalDateTime?
) {

    @SuppressLint("NewApi")
    fun toEntity(): Product {
        //val t = createdDateTime?.toInstant(TimeZone.UTC)?.epochSeconds
        return Product(id, isDeleted, fridgeId, barcode, name, amount, null)
    }

    companion object {
        fun fromEntity(entity : Product) : ProductModel {

            //var t : LocalDateTime? = null
            entity.createdTimestamp?.let {
                //t = Instant.fromEpochSeconds(it, 0)
                //    .toLocalDateTime(TimeZone.currentSystemDefault())
            }

            return ProductModel(entity.id, entity.isDeleted, entity.fridgeId, entity.barcode, entity.name,entity.amount/*, t?:null*/)
        }
    }
}