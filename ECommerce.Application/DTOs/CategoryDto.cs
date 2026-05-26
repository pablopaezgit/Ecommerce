namespace ECommerce.Application.DTOs;

public record CreateCategoryDto(
    string Name,
    string Description);

public record CategoryResponseDto(
    Guid   Id,
    string Name,
    string Description);
