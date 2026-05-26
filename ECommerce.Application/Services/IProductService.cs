// Application/Services/IProductService.cs
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<ProductResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ProductResponseDto> CreateAsync(CreateProductDto dto, CancellationToken ct = default);
    Task UpdateAsync(Guid id, UpdateProductDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
