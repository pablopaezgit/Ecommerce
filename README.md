# ECommerce Clean Architecture API

Backend de e-commerce desarrollado con ASP.NET Core 8 aplicando Clean Architecture, Entity Framework Core y principios de arquitectura escalable.

---

# Tecnologías utilizadas

- ASP.NET Core 8
- C#
- Entity Framework Core 8
- SQLite
- Swagger / OpenAPI
- Git
- GitHub

---

# Arquitectura utilizada

El proyecto sigue el patrón **Clean Architecture** separando responsabilidades en distintas capas:

```text
ECommerce
│
├── ECommerce.Api
├── ECommerce.Application
├── ECommerce.Domain
└── ECommerce.Infrastructure

Capas
Api

Contiene:

Controllers
configuración de Swagger
endpoints HTTP
configuración de la aplicación
Application

Contiene:

interfaces
contratos
lógica de aplicación
Domain

Contiene:

entidades
reglas de negocio
lógica del dominio
Infrastructure

Contiene:

Entity Framework Core
DbContext
Repositories
Migraciones
acceso a datos
Métodos y patrones utilizados
Clean Architecture

Separación de responsabilidades por capas para lograr:

mantenibilidad
escalabilidad
bajo acoplamiento
Repository Pattern

Se implementó el patrón Repository para abstraer el acceso a datos.

Ejemplo:

IProductRepository
ProductRepository
Dependency Injection

Uso de inyección de dependencias nativa de ASP.NET Core mediante:

builder.Services.AddInfrastructure(builder.Configuration);
Entity Framework Core

Se utilizó EF Core para:

mapeo objeto-relacional
consultas
persistencia
migraciones
Fluent API

Configuración avanzada de entidades mediante Fluent API:

IEntityTypeConfiguration<T>

Permite:

configurar relaciones
índices
restricciones
tipos SQL
Migraciones

Se utilizaron migraciones de EF Core para generar automáticamente la base de datos.

Comandos utilizados:

dotnet ef migrations add InitialCreate
dotnet ef database update
Swagger / OpenAPI

Documentación automática de endpoints REST mediante Swagger UI.

Funcionalidades implementadas
Productos
Obtener todos los productos
Obtener producto por ID
Crear producto
Eliminar producto
Base de datos

Se utilizó SQLite como motor de base de datos.

Archivo generado:

ecommerce.db
Endpoints disponibles
Product
GET /api/Product
GET /api/Product/{id}
POST /api/Product
DELETE /api/Product/{id}
Ejecutar proyecto
Restaurar paquetes
dotnet restore
Aplicar migraciones
dotnet ef database update --project ECommerce.Infrastructure --startup-project ECommerce.Api
Ejecutar API
dotnet run --project ECommerce.Api
Swagger
https://localhost:xxxx/swagger
Objetivo del proyecto

Proyecto realizado con fines de aprendizaje y práctica profesional de desarrollo backend utilizando tecnologías modernas del ecosistema .NET.
