using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Queries.Order;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderResponseDto>;

public record GetOrdersByUserQuery(Guid UserId) : IRequest<IEnumerable<OrderResponseDto>>;