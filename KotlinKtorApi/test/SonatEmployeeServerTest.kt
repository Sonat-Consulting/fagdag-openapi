package dev.sonat.employee.api

import java.util.*
import io.ktor.config.*
import io.ktor.http.*
import io.ktor.request.*
import io.ktor.server.testing.*
import io.ktor.swagger.experimental.*
import kotlin.test.*

class SwaggerRoutesTest {
    /**
     * @see SonatEmployeeServer.getEmployees
     */
    @Test
    fun testGetEmployees() {
        withTestApplication {
            // @TODO: Adjust path as required
            handleRequest(HttpMethod.Get, "/employees") {
            }.apply {
                // @TODO: Your test here
                assertEquals(HttpStatusCode.OK, response.status())
            }
        }
    }

    /**
     * @see SonatEmployeeServer.addEmployee
     */
    @Test
    fun testAddEmployee() {
        withTestApplication {
            // @TODO: Adjust path as required
            handleRequest(HttpMethod.Post, "/employees") {
                // @TODO: Your body here
                setBodyJson(mapOf<String, Any?>())
            }.apply {
                // @TODO: Your test here
                assertEquals(HttpStatusCode.OK, response.status())
            }
        }
    }

    /**
     * @see SonatEmployeeServer.updateEmployee
     */
    @Test
    fun testUpdateEmployee() {
        withTestApplication {
            // @TODO: Adjust path as required
            handleRequest(HttpMethod.Put, "/employees") {
                // @TODO: Your body here
                setBodyJson(mapOf<String, Any?>())
            }.apply {
                // @TODO: Your test here
                assertEquals(HttpStatusCode.OK, response.status())
            }
        }
    }

    /**
     * @see SonatEmployeeServer.getEmployeeById
     */
    @Test
    fun testGetEmployeeById() {
        withTestApplication {
            // @TODO: Adjust path as required
            handleRequest(HttpMethod.Get, "/employees/{employeeId}") {
            }.apply {
                // @TODO: Your test here
                assertEquals(HttpStatusCode.OK, response.status())
            }
        }
    }

    /**
     * @see SonatEmployeeServer.deleteEmployee
     */
    @Test
    fun testDeleteEmployee() {
        withTestApplication {
            // @TODO: Adjust path as required
            handleRequest(HttpMethod.Delete, "/employees/{employeeId}") {
            }.apply {
                // @TODO: Your test here
                assertEquals(HttpStatusCode.OK, response.status())
            }
        }
    }

    fun <R> withTestApplication(test: TestApplicationEngine.() -> R): R {
        return withApplication(createTestEnvironment()) {
            (environment.config as MapApplicationConfig).apply {
                put("jwt.secret", "TODO-change-this-supersecret-or-use-SECRET-env")
            }
            application.module()
            test()
        }
    }

    fun TestApplicationRequest.setBodyJson(value: Any?) = setBody(Json.stringify(value))
}
