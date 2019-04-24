# Intro

Du har fått i oppdrag å lage et api for håndtering av ansattinformasjon for Sonat Consulting. 

# Api skal tilby følgene operasjoner
- utlisting av alle ansatte
- uthenting av ansatt på bakgrunn av id
- sletting av ansatt på bakgrunn av id
- oppdatering av ansatt på bakgrunn av id

En ansatt representeres som Employee objekt med følgende egenskaper:
####    Employee

        { Id, FirstName, LastName }

# Oppgave 1
Lag en [OpenAPI spesifikasjon](https://swagger.io/docs/specification/about/) som definerer kontrakten. Om du ønsker, kan du benytte [SwaggerEditor](https://editor.swagger.io).

# Oppgave 2
Implementer api som du definerte i Oppgave 1. Programmeringsspråk er valgfritt. 

Du finner eksempelkode for:
- [.net core](https://github.com/Sonat-Consulting/fagdag-openapi/tree/master/NetCoreApi)
- [Kotlin](https://github.com/Sonat-Consulting/fagdag-openapi/tree/master/KotlinKtorApi)
- [Node.js](https://github.com/Sonat-Consulting/fagdag-openapi/tree/master/NodeApi)


# Oppgave 4
#### Sikre tilgangen til APIet ditt med autentisering og tilgangsstyring.

Følgende oppsett fra Auth0 kan brukes:    
**Authority/Domain:** https://sonat.eu.auth0.com/    
**Audience:** https://auth.sonat.dev

For å få en token, kan du logge inn på denne siden:   
https://auth-tester.sonat.dev

På siden får du se din id token og access token. Du kan se på innhold av 
tokenet på https://jwt.io

Access tokenet skal ha et permissions-claim, som inneholder `read:employees`.
I tillegg er der definert en tilgang som hetter `modify:employees`. Denne 
kan settes opp manuelt. Gi en lyd til arrangørende av fagdagen 
for å gjøre dette for en bruker.

Sett opp tilgang slik:
* at brukere med `read:employee` permissions får tilgang til `GET` 
endepunktene i APIet.
* og brukere med `modify:employee` permissions får tilgang til `POST`, 
`PUT` og `DELETE` endepunktene.

Der finnes eksempel på hvordan dette gjøres for .net her:   
https://auth0.com/docs/quickstart/backend/aspnet-core-webapi

Se også gjerne på eksempel implementasjonene vi har laget på github:
https://github.com/Sonat-Consulting/fagdag-openapi
