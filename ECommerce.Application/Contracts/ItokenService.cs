using ECommerce.Domain.Entities;

namespace ECommerce.Application.Contracts;

// Contrato definido en Application, implementado en Infrastructure.
// Así Application no depende de JWT ni de ninguna librería externa.
public interface ITokenService
{
    string GenerateToken(User user);
}
