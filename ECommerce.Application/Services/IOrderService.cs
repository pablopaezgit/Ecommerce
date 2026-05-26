// Application/Services/IOrderService.cs
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services;

public interface IOrderService
{
    Task<OrderResponseDto> CreateAsync(CreateOrderDto dto, CancellationToken ct = default);
    Task<OrderResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<OrderResponseDto>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
}
