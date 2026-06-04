using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Commands.Product;

// IRequest<T> indica qué tipo devuelve el Handler
public record CreateProductCommand(
    string  Name,
    string  Description,
    decimal Price,
    int     Stock,
    Guid    CategoryId) : IRequest<ProductResponseDto>;

public record UpdateProductCommand(
    Guid    Id,
    string  Name,
    string  Description,
    decimal Price,
    int     Stock) : IRequest;

public record DeleteProductCommand(Guid Id) : IRequest;