using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Services;

public class CategoryService : ICategoryService 
{
    private readonly ICategoryRepository _categories;
    private readonly IUnitOfWork         _uow;

    public CategoryService(ICategoryRepository categories, IUnitOfWork uow)
    {
        _categories = categories;
        _uow        = uow;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var cats = await _categories.GetAllAsync(ct);
        return cats.Select(c => new CategoryResponseDto(c.Id, c.Name, c.Description));
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto, CancellationToken ct = default)
    {
        // Regla de negocio: no pueden existir dos categorías con el mismo nombre
        if (await _categories.ExistsByNameAsync(dto.Name, ct))
            throw new BusinessRuleException($"Ya existe una categoría con el nombre '{dto.Name}'.");

        var category = new Category(dto.Name, dto.Description);

        await _categories.AddAsync(category, ct);
        await _uow.SaveChangesAsync(ct);

        return new CategoryResponseDto(category.Id, category.Name, category.Description);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var category = await _categories.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Category), id);

        await _categories.DeleteAsync(category.Id, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
