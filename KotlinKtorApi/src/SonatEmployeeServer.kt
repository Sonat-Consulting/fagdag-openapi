package dev.sonat.employee.api

import io.ktor.application.*
import io.ktor.response.*
import io.ktor.routing.*
import java.util.*
import io.ktor.swagger.experimental.*
import io.ktor.auth.*
import io.ktor.http.*

/**
 * Sonat employee
 * 
 * Sonat employee api
 */
class SonatEmployeeServer() {
    /**
     * Employee
     */
    fun Routing.registerEmployee() {
        get("/employees") {
            call.respond(listOf(Employee(1, "Lars", "Aaberg")))
        }

        post("/employees") {
            if (false) httpException(HttpStatusCode.Created)
            if (false) httpException(HttpStatusCode.BadRequest)

            call.respond("")
        }

        put("/employees") {
            if (false) httpException(HttpStatusCode.NoContent)
            if (false) httpException(HttpStatusCode.BadRequest)
            if (false) httpException(HttpStatusCode.NotFound)

            call.respond("")
        }

        get("/employees/{employeeId}") {
            val employeeId = call.getPath<Long>("employeeId") 

            if (false) httpException(HttpStatusCode.BadRequest)
            if (false) httpException(HttpStatusCode.NotFound)

            call.respond(Employee(
                id = 0,
                firstName = "firstName",
                lastName = "lastName"
            ))
        }

        delete("/employees/{employeeId}") {
            val employeeId = call.getPath<Long>("employeeId") 

            if (false) httpException(HttpStatusCode.NoContent)
            if (false) httpException(HttpStatusCode.BadRequest)
            if (false) httpException(HttpStatusCode.NotFound)

            call.respond("")
        }
    }
}
