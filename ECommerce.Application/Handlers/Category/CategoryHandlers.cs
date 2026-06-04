using ECommerce.Application.Commands.Category;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Application.Queries.Category;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Handlers.Category;

// ── Create ────────────────────────────────────────────────────────────────
public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponseDto>
{
    private readonly ICategoryRepository _categories;
    private readonly IUnitOfWork         _uow;

    public CreateCategoryHandler(ICategoryRepository categories, IUnitOfWork uow)
    {
        _categories = categories;
        _uow        = uow;
    }

    public async Task<CategoryResponseDto> Handle(CreateCategoryCommand cmd, CancellationToken ct)
    {
        if (await _categories.ExistsByNameAsync(cmd.Name, ct))
            throw new Domain.Exceptions.BusinessRuleException(
                $"Ya existe una categoría con el nombre '{cmd.Name}'.");

        var category = new Domain.Entities.Category(cmd.Name, cmd.Description);

        await _categories.AddAsync(category, ct);
        await _uow.SaveChangesAsync(ct);

        return new CategoryResponseDto(category.Id, category.Name, category.Description);
    }
}

// ── Delete ────────────────────────────────────────────────────────────────
public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categories;
    private readonly IUnitOfWork         _uow;

    public DeleteCategoryHandler(ICategoryRepository categories, IUnitOfWork uow)
    {
        _categories = categories;
        _uow        = uow;
    }

    public async Task Handle(DeleteCategoryCommand cmd, CancellationToken ct)
    {
        var category = await _categories.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException(nameof(Domain.Entities.Category), cmd.Id);

        await _categories.DeleteAsync(category.Id, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

// ── GetAll ────────────────────────────────────────────────────────────────
public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponseDto>>
{
    private readonly ICategoryRepository _categories;

    public GetAllCategoriesHandler(ICategoryRepository categories) => _categories = categories;

    public async Task<IEnumerable<CategoryResponseDto>> Handle(
        GetAllCategoriesQuery query, CancellationToken ct)
    {
        var cats = await _categories.GetAllAsync(ct);
        return cats.Select(c => new CategoryResponseDto(c.Id, c.Name, c.Description));
    }
}
