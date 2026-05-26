using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository   _orders;
    private readonly IProductRepository _products;
    private readonly IUnitOfWork        _uow;

    public OrderService(
        IOrderRepository   orders,
        IProductRepository products,
        IUnitOfWork        uow)
    {
        _orders   = orders;
        _products = products;
        _uow      = uow;
    }

    public async Task<OrderResponseDto> CreateAsync(CreateOrderDto dto, CancellationToken ct = default)
    {
        var order = new Order(dto.UserId);

        foreach (var item in dto.Items)
        {
            var product = await _products.GetByIdAsync(item.ProductId, ct)
                ?? throw new NotFoundException(nameof(Product), item.ProductId);

            // AddItem ya descuenta el stock internamente (lógica en la entidad)
            order.AddItem(product, item.Quantity);
        }

        await _orders.AddAsync(order, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(order);
    }

    public async Task<OrderResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var order = await _orders.GetWithItemsAsync(id, ct)
            ?? throw new NotFoundException(nameof(Order), id);

        return ToDto(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        var orders = await _orders.GetByUserIdAsync(userId, ct);
        return orders.Select(ToDto);
    }

    private static OrderResponseDto ToDto(Order o) => new(
        o.Id,
        o.UserId,
        o.Status.ToString(),
        o.Total,
        o.CreatedAt,
        o.Items.Select(i => new OrderItemResponseDto(
            i.ProductId, i.UnitPrice, i.Quantity, i.Subtotal)).ToList());
}
