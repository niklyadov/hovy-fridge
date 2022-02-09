package com.niklyadov.hovy_fridge_app.Entity

data class Student(
    val name : String,

    val expelled : Boolean
) {
    override fun toString(): String {
        return "Студент $name, отчислен: ${if (expelled) "да" else "нет"}";
    }
}