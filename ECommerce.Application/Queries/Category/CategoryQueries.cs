using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Queries.Category;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryResponseDto>>;