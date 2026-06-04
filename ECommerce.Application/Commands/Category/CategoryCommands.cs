using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Commands.Category;

public record CreateCategoryCommand(
    string Name,
    string Description) : IRequest<CategoryResponseDto>;

public record DeleteCategoryCommand(Guid Id) : IRequest;
