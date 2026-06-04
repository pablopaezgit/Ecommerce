using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Queries.Product;

// IRequest<T> — T es lo que devuelve el Handler
public record GetAllProductsQuery() : IRequest<IEnumerable<ProductResponseDto>>;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponseDto>;
