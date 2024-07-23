### Cottage Hotel Management Project

# This project aims to implement a part of the management of a cottage hotel, focusing primarily on the functionalities related to the cottages and their maintenance.

## Important Information

A series of specific requirements are attached, which are detailed in the project outline provided by the Faculty of Engineering.

# Main Requirements
## Refactoring with Clean Architecture

Adaptation of business logic.
Implementation of Value Object for specific attributes.
Use of Interfaces for repositories.
Use of custom exceptions for validations. 

### Application Layer
Inclusion of existing use cases and those added in this stage.
### Clean Architecture and Layer Communication

Exclusive use of abstractions (Interfaces) and Dependency Injection.
### Web APIs and Queries

Implementation of CRUD operations with REST orientation.
Specific queries in Linq and exposure through Web API.
### Security and Documentation

Implementation of JWT Token for API security.
Comprehensive documentation with Open API specification and method comments.
### HttpClient Client

Consumption of Web APIs in a Web MVC project.
Use of View Models to process HTTP responses and display error messages.
# Requirements to Implement
The project aims to implement the following requirements:

### User Registration
Login and Logout for identified and unidentified users.
Maintenance of cottage types.
Registration of cottages and search according to various criteria.
Registration and search of maintenance performed on cottages.
Listing of maintenance between dates.
# Implementation
## Technologies Used
Platform: Visual Studio 2022
Language: C# (.NET 7)
Database: SQL Server with SqlClient provider
## Project Structure
The project is structured in layers that include:

Persistence classes / Repositories.
MVC Controllers.
Domain classes.
## Documentation
In the Documentation folder, there is a PDF with the functional requirements, as well as class diagrams that model the domain and persistence solution.

## Test Data
SQL scripts are provided for the creation of tables and test data for the application, ensuring its viability during the defense.

### DATA FOR USING THE APPLICATION
### Users
<table style="width:100%">
<tr>
<td>MAIL</td>
<td>PASSWORD</td>
</tr>
<tr>
<td>forihuela@gmail.com</td>
<td>Facu1234</td>
</tr>
<tr>
<td>fmillot@gmail.com</td>
<td>Fede1234</td>
</tr>
<tr>
<td>prueba@gmail.com</td>
<td>Abcd1234</td>
</tr>
</table>










----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### Proyecto Gestión de Hotel de Cabañas

# Este proyecto tiene como objetivo implementar una parte de la gestión de un hotel de cabañas, enfocándose principalmente en las funcionalidades relativas a las cabañas y sus mantenimientos.

## Información Importante
Se adjunta una serie de requerimientos específicos, los cuales se detallan en la letra del proyecto proporcionada por la Facultad de Ingeniería.

# Requerimientos Principales
## Refactorización con Arquitectura Limpia

Adaptación de la lógica de negocio.
Implementación de Value Object para atributos específicos.
Uso de Interfaces para los repositorios.
Empleo de excepciones personalizadas para validaciones.
### Capa de Aplicación

Inclusión de casos de uso existentes y los agregados en esta etapa.
### Arquitectura Limpia y Comunicación entre Capas

Exclusivo uso de abstracciones (Interfaces) y Dependency Injection.
### APIs Web y Consultas

Implementación de operaciones CRUD con orientación REST.
Consultas específicas en Linq y exposición a través de Web Api.
### Seguridad y Documentación

Implementación de JWT Token para seguridad en las APIs.
Documentación exhaustiva con especificación Open Api y comentarios en métodos.
### Cliente HttpClient

Consumo de las Web APIs en un proyecto Web MVC.
Uso de View Models para procesar las respuestas HTTP y mostrar mensajes de error.
# Requerimientos a Implementar
El proyecto busca implementar los siguientes requerimientos:

### Registro de usuarios.
Login y Logout para usuarios identificados y no identificados.
Mantenimiento de tipos de cabañas.
Registro de cabañas y búsqueda según diversos criterios.
Registro y búsqueda de mantenimientos realizados a las cabañas.
Listado de mantenimientos entre fechas.
# Implementación
## Tecnologías Utilizadas
Plataforma: Visual Studio 2022
Lenguaje: C# (.NET 7)
Base de Datos: SQL Server con proveedor SqlClient
## Estructura del Proyecto
El proyecto está estructurado en capas que incluyen:

Clases de persistencia / Repositorios.
Controllers de MVC.
Clases del dominio.
## Documentación
En la carpeta Documentacion se encuentra el PDF con los requerimientos funcionales, así como los diagramas de clases que modelan la solución de dominio y persistencia.


## Datos de Prueba
Se proveen scripts SQL para la creación de tablas y datos de prueba para la aplicación, asegurando la viabilidad de su funcionamiento en la defensa.
 ###  DATOS PARA USO EL USO DE L A APLICACIÓN
### Usuarios
<table style="width:100%">
<tr>
<td>
    MAIL
</td>
 
<td>
  PASSWORD
</td>

</tr>
<tr>
<td>
  forihuela@gmail.com
</td>
<td>
  Facu1234
</td>
</tr>
<tr>
<td>
  fmillot@gmail.com
</td>
<td>
  Fede1234
</td>
</tr>
<tr>
<td>
  prueba@gmail.com
</td>
<td>
  Abcd1234
</td>
</tr>
</table>

 
