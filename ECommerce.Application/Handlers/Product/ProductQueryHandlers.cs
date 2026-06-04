using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Application.Queries.Product;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.Handlers.Product;

// ── GetAll ────────────────────────────────────────────────────────────────
public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IProductRepository _products;

    public GetAllProductsHandler(IProductRepository products) => _products = products;

    public async Task<IEnumerable<ProductResponseDto>> Handle(
        GetAllProductsQuery query, CancellationToken ct)
    {
        var products = await _products.GetAllAsync(ct);
        return products.Select(p =>
            new ProductResponseDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.CreatedAt));
    }
}

// ── GetById ───────────────────────────────────────────────────────────────
public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
{
    private readonly IProductRepository _products;

    public GetProductByIdHandler(IProductRepository products) => _products = products;

    public async Task<ProductResponseDto> Handle(
        GetProductByIdQuery query, CancellationToken ct)
    {
        var p = await _products.GetByIdAsync(query.Id, ct)
            ?? throw new NotFoundException(nameof(Domain.Entities.Product), query.Id);

        return new ProductResponseDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.CreatedAt);
    }
}
