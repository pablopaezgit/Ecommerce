using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        => await _set
            .AsNoTracking()
            .Include(o => o.Items) // carga los items en la misma consulta
            .Where(o => o.UserId == userId)
            .ToListAsync(ct);

    public async Task<Order?> GetWithItemsAsync(Guid id, CancellationToken ct = default)
        => await _set
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, ct);
}