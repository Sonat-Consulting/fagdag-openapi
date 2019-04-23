package dev.sonat.employee.api

import io.ktor.client.*
import io.ktor.client.request.*

/**
 * Sonat employee Client
 * 
 * Sonat employee api
 */
open class SonatEmployeeClient(val endpoint: String, val client: HttpClient = HttpClient()) {
    /**
     * Returns all employees
     * 
     * @return successful
     */
    suspend fun getEmployees(
    ): List<Employee> {
        return client.get<List<Employee>>("$endpoint/employees") {
        }
    }

    /**
     * Add a new employee
     * 
     * @return OK
     */
    suspend fun addEmployee(
    ): String {
        return client.post<String>("$endpoint/employees") {
        }
    }

    /**
     * Update an existing employee
     * 
     * @return OK
     */
    suspend fun updateEmployee(
    ): String {
        return client.put<String>("$endpoint/employees") {
        }
    }

    /**
     * Find employee by ID
     * 
     * Returns a single employee
     * 
     * @param employeeId ID of employee to return
     * 
     * @return successful operation
     */
    suspend fun getEmployeeById(
        employeeId: Long // PATH
    ): Employee {
        return client.get<Employee>("$endpoint/employees/$employeeId") {
        }
    }

    /**
     * Delete an employee
     * 
     * @param employeeId Employee id to delete
     * 
     * @return OK
     */
    suspend fun deleteEmployee(
        employeeId: Long // PATH
    ): String {
        return client.delete<String>("$endpoint/employees/$employeeId") {
        }
    }
}
