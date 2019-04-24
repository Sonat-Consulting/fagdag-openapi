package dev.sonat.employee.api

import com.auth0.jwk.JwkProviderBuilder
import com.auth0.jwk.UrlJwkProvider
import io.ktor.application.*
import io.ktor.response.*
import io.ktor.request.*
import io.ktor.features.*
import io.ktor.routing.*
import io.ktor.http.*
import io.ktor.auth.*
import io.ktor.client.*
import io.ktor.client.engine.apache.*
import io.ktor.client.features.json.*
import io.ktor.client.request.*
import kotlinx.coroutines.*
import com.fasterxml.jackson.databind.*
import io.ktor.auth.jwt.jwt
import io.ktor.jackson.*
import kotlin.reflect.*
import java.util.*
import io.ktor.swagger.experimental.*
import com.auth0.jwt.*
import com.auth0.jwt.algorithms.*
import io.ktor.auth.jwt.JWTCredential
import io.ktor.auth.jwt.JWTPrincipal
import java.io.Console
import java.util.concurrent.TimeUnit

fun main(args: Array<String>): Unit = io.ktor.server.netty.EngineMain.main(args)

@Suppress("unused") // Referenced in application.conf
@kotlin.jvm.JvmOverloads
fun Application.module(testing: Boolean = false) {
    install(DefaultHeaders) {
        header("X-Engine", "Ktor") // will send this header with each response
    }

    val jwkRealm = "https://auth.sonat.dev"
    val jwkIssuer = "https://sonat.eu.auth0.com/"
    val audience = "https://auth.sonat.dev/"
    val jwkProvider = JwkProviderBuilder(jwkIssuer)
        .cached(10, 24, TimeUnit.HOURS)
        .rateLimited(10, 1, TimeUnit.MINUTES)
        .build()

    install(Authentication) {
        jwt (name = "auth") {
            //realm = jwkRealm
            verifier(jwkProvider, jwkIssuer)
            validate{ credentials ->
                validateJwt(credentials)
            }
        }
    }

    val client = HttpClient(Apache) {
        install(JsonFeature) {
            serializer = GsonSerializer()
        }
    }
    runBlocking {
        // Sample for making a HTTP Client request
        /*
        val message = client.post<JsonSampleClass> {
            url("http://127.0.0.1:8080/path/to/endpoint")
            contentType(ContentType.Application.Json)
            body = JsonSampleClass(hello = "world")
        }
        */
    }

    install(ContentNegotiation) {
        jackson {
            enable(SerializationFeature.INDENT_OUTPUT)
        }
    }

    routing {
        get("/") {
            call.respondText("HELLO WORLD!", contentType = ContentType.Text.Plain)
        }

        install(StatusPages) {
            exception<AuthenticationException> { cause ->
                call.respond(HttpStatusCode.Unauthorized)
            }
            exception<AuthorizationException> { cause ->
                call.respond(HttpStatusCode.Forbidden)
            }

            exception<HttpException> {  cause ->
                call.respond(cause.code, cause.description)
            }

        }

        get("/json/jackson") {
            call.respond(mapOf("hello" to "world"))
        }

        SonatEmployeeServer().apply {
            registerEmployee()
        }
    }
}

fun validateJwt(credentials: JWTCredential) : Principal? {

    if (credentials.payload.issuer.equals("https://sonat.eu.auth0.com/"))
        return JWTPrincipal(credentials.payload)
    else
        return null
}

data class JsonSampleClass(val hello: String)

class AuthenticationException : RuntimeException()
class AuthorizationException : RuntimeException()

