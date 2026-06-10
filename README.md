# ECommerce — Clean Architecture API

Backend de e-commerce desarrollado con ASP.NET Core 8, aplicando Clean Architecture, CQRS con MediatR y Entity Framework Core.

---

## Tecnologías

| Tecnología | Versión |
|---|---|
| ASP.NET Core | 8.0 |
| Entity Framework Core | 8.0 |
| MediatR | 14.x |
| BCrypt.Net-Next | 4.x |
| SQLite | — |
| Swagger / OpenAPI | — |

---

## Arquitectura

El proyecto sigue *Clean Architecture* con cuatro capas. Las dependencias apuntan siempre hacia adentro.


ECommerce.Domain          ← Entidades, Value Objects, excepciones de dominio
ECommerce.Application     ← Casos de uso, Commands, Queries, Handlers, DTOs
ECommerce.Infrastructure  ← EF Core, repositorios, JWT, migraciones
Ecommerce.Api             ← Controllers, Middleware, Program.cs


### Flujo HTTP


HTTP Request
    → GlobalExceptionHandler (atrapa errores)
    → RequestLoggingMiddleware (loguea método, ruta y tiempo)
    → AuthenticationMiddleware (valida JWT)
    → Controller (recibe y delega)
    → MediatR (enruta al Handler)
    → Handler (orquesta la lógica)
    → Domain (reglas de negocio)
    → Repository → SQLite
    ← HTTP Response (200 / 201 / 204 / 400 / 404)


---

## Patrones implementados

*CQRS con MediatR* — Los controllers no tienen lógica de negocio. Solo crean un Command o Query y lo envían por _mediator.Send(). El Handler correspondiente resuelve la operación.

*Repository Pattern* — La capa Application define interfaces (IProductRepository, IUserRepository, etc.). Infrastructure las implementa con EF Core. Application nunca conoce EF Core.

*Unit of Work* — IUnitOfWork centraliza el SaveChangesAsync para garantizar que los cambios se persistan de forma atómica.

*GlobalExceptionHandler* — Implementa IExceptionHandler de ASP.NET Core. Devuelve respuestas en formato ProblemDetails (RFC 7807) con mapeo de excepciones de dominio a códigos HTTP:

| Excepción | HTTP |
|---|---|
| NotFoundException | 404 |
| BusinessRuleException | 400 |
| DomainException | 422 |
| Cualquier otra | 500 |

---

## Seguridad

- Passwords hasheados con *BCrypt* (BCrypt.Net-Next)
- Autenticación con *JWT Bearer*

---

## Endpoints

### Auth
| Método | Ruta | Auth | Descripción |
|---|---|---|---|
| POST | /api/Auth/register | No | Registrar usuario |
| POST | /api/Auth/login | No | Login, devuelve JWT |

### Products
| Método | Ruta | Auth | Descripción |
|---|---|---|---|
| GET | /api/Product | No | Listar productos |
| GET | /api/Product/{id} | No | Obtener por ID |
| POST | /api/Product | Sí | Crear producto |
| PUT | /api/Product/{id} | Sí | Actualizar producto |
| DELETE | /api/Product/{id} | Sí | Eliminar producto |

### Categories
| Método | Ruta | Auth | Descripción |
|---|---|---|---|
| GET | /api/Category | No | Listar categorías |
| POST | /api/Category | Sí | Crear categoría |
| DELETE | /api/Category/{id} | Sí | Eliminar categoría |

### Orders
| Método | Ruta | Auth | Descripción |
|---|---|---|---|
| POST | /api/Order | Sí | Crear orden |
| GET | /api/Order/{id} | Sí | Obtener orden por ID |

---

## Cómo correr el proyecto

*Requisito:* .NET 8 SDK instalado.

*1.* Clonar el repositorio y abrir una terminal en la carpeta Ecommerce.Api:

git clone https://github.com/pablopaezgit/Ecommerce.git


cd ECommerce/Ecommerce.Api


*2.* Ejecutar:

dotnet run


*3.* Abrir Swagger en el navegador:


http://localhost:5174/swagger


*4.* Registrar un usuario en POST /api/Auth/register, hacer login en POST /api/Auth/login, copiar el token y pegarlo en el botón *Authorize* con el formato Bearer <token>.
