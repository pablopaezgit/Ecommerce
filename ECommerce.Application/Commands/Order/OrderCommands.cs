using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Commands.Order;

public record CreateOrderCommand(
    Guid                      UserId,
    List<OrderItemRequestDto> Items) : IRequest<OrderResponseDto>;