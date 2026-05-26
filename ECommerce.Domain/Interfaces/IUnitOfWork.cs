namespace ECommerce.Domain.Interfaces;

// Unit of Work agrupa varias operaciones en una transacción atómica.
// Si algo falla, todo se revierte. Solo hay UN SaveChanges al final.
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
