using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Contracts.Persistence;

// Extiende IRepository<T> y agrega queries específicas de Product
public interface IProductRepository : IRepository<Product>
{
    // Buscar productos por categoría
    Task<IEnumerable<Product>> GetByCategoryAsync(
        Guid categoryId,
        CancellationToken ct = default);

    // Verificar si hay stock suficiente
    Task<bool> HasStockAsync(
        Guid productId,
        int  quantity,
        CancellationToken ct = default);
}

// Queries específicas de Category
public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken ct = default);
}

// Queries específicas de User
public interface IUserRepository : IRepository<User>
{
    // Login necesita buscar por email
    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken ct = default);

    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken ct = default);
}

// Queries específicas de Order
public interface IOrderRepository : IRepository<Order>
{
    // Historial de compras de un usuario, con los items cargados
    Task<IEnumerable<Order>> GetByUserIdAsync(
        Guid userId,
        CancellationToken ct = default);

    // Detalle de una orden con sus items (eager loading)
    Task<Order?> GetWithItemsAsync(
        Guid id,
        CancellationToken ct = default);
}
