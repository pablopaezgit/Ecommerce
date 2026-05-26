using System.Security.Cryptography;
using System.Text;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork     _uow;

    public UserService(IUserRepository users, IUnitOfWork uow)
    {
        _users = users;
        _uow   = uow;
    }

    public async Task<UserResponseDto> RegisterAsync(RegisterUserDto dto, CancellationToken ct = default)
    {
        // No puede existir otro usuario con el mismo email
        if (await _users.ExistsByEmailAsync(dto.Email, ct))
            throw new BusinessRuleException($"El email '{dto.Email}' ya está registrado.");

        // Hash simple con SHA256 (en producción usarías BCrypt o ASP.NET Identity)
        var hash = HashPassword(dto.Password);

        var user = new User(dto.Email, dto.Name, hash);

        await _users.AddAsync(user, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(user);
    }

    public async Task<User> ValidateCredentialsAsync(LoginDto dto, CancellationToken ct = default)
    {
        var user = await _users.GetByEmailAsync(dto.Email, ct)
            ?? throw new BusinessRuleException("Credenciales inválidas.");

        if (user.PasswordHash != HashPassword(dto.Password))
            throw new BusinessRuleException("Credenciales inválidas.");

        return user;
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }

    private static UserResponseDto ToDto(User u) =>
        new(u.Id, u.Name, u.Email, u.Role, u.CreatedAt);
}
