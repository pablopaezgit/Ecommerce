namespace ECommerce.Application.DTOs;

// Un ítem dentro de la orden que envía el cliente
public record OrderItemRequestDto(
    Guid ProductId,
    int  Quantity);

// Para crear una orden
public record CreateOrderDto(
    Guid                       UserId,
    List<OrderItemRequestDto>  Items);

// Ítem de orden que devuelve la API
public record OrderItemResponseDto(
    Guid    ProductId,
    decimal UnitPrice,
    int     Quantity,
    decimal Subtotal);

// Respuesta completa de una orden
public record OrderResponseDto(
    Guid                        Id,
    Guid                        UserId,
    string                      Status,
    decimal                     Total,
    DateTime                    CreatedAt,
    List<OrderItemResponseDto>  Items);
