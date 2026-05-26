using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext ctx) : base(ctx) { }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await _set
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant(), ct);

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
        => await _set.AnyAsync(u => u.Email == email.ToLowerInvariant(), ct);
}