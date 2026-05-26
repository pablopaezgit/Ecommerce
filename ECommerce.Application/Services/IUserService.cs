// Application/Services/IUserService.cs
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public interface IUserService
{
    Task<UserResponseDto> RegisterAsync(RegisterUserDto dto, CancellationToken ct = default);
    Task<User> ValidateCredentialsAsync(LoginDto dto, CancellationToken ct = default);
}
