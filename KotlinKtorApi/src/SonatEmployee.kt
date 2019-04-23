package dev.sonat.employee.api

import java.util.*
import io.ktor.http.*
import io.ktor.request.*
import io.ktor.swagger.experimental.*

data class Employee(
    val id: Long,
    val firstName: String,
    val lastName: String
)
