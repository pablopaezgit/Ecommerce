namespace ECommerce.Application.DTOs;

// Lo que el cliente ENVÍA para crear un producto
public record CreateProductDto(
    string  Name,
    string  Description,
    decimal Price,
    int     Stock,
    Guid    CategoryId);

// Lo que el cliente ENVÍA para actualizar un producto
public record UpdateProductDto(
    string  Name,
    string  Description,
    decimal Price,
    int     Stock);

// Lo que la API DEVUELVE al cliente
public record ProductResponseDto(
    Guid     Id,
    string   Name,
    string   Description,
    decimal  Price,
    int      Stock,
    Guid     CategoryId,
    DateTime CreatedAt);
