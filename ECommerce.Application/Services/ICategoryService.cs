// Application/Services/ICategoryService.cs
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
