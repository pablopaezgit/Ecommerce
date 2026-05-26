using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _products;
    private readonly IUnitOfWork        _uow;

    public ProductService(IProductRepository products, IUnitOfWork uow)
    {
        _products = products;
        _uow      = uow;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var products = await _products.GetAllAsync(ct);
        return products.Select(ToDto);
    }

    public async Task<ProductResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var product = await _products.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Product), id);

        return ToDto(product);
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto, CancellationToken ct = default)
    {
        var product = new Product(dto.Name, dto.Description, dto.Price, dto.Stock, dto.CategoryId);

        await _products.AddAsync(product, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(product);
    }

    public async Task UpdateAsync(Guid id, UpdateProductDto dto, CancellationToken ct = default)
    {
        var product = await _products.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Product), id);

        product.UpdatePrice(dto.Price);

        await _products.UpdateAsync(product, ct);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var product = await _products.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Product), id);

        await _products.DeleteAsync(product.Id, ct);
        await _uow.SaveChangesAsync(ct);
    }

    // Mapeo de entidad a DTO (sin AutoMapper para mantenerlo simple)
    private static ProductResponseDto ToDto(Product p) => new(
        p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.CreatedAt);
}
