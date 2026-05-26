namespace ECommerce.Domain.Interfaces;

// T debe ser una clase (entidad). Este contrato define las operaciones
// básicas de persistencia sin mencionar EF Core ni ninguna tecnología.
public interface IRepository<T> where T : class
{
    Task<T?>              GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    Task                  AddAsync(T entity, CancellationToken ct = default);
    Task                  UpdateAsync(T entity, CancellationToken ct = default);
    Task                  DeleteAsync(Guid id, CancellationToken ct = default);
}
