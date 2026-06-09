using ECommerce.Application.Commands.Auth;
using ECommerce.Application.Contracts;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Handlers.Auth;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserResponseDto>
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork     _uow;

    public RegisterUserHandler(IUserRepository users, IUnitOfWork uow)
    {
        _users = users;
        _uow   = uow;
    }

    public async Task<UserResponseDto> Handle(RegisterUserCommand cmd, CancellationToken ct)
    {
        if (await _users.ExistsByEmailAsync(cmd.Email, ct))
            throw new BusinessRuleException($"El email '{cmd.Email}' ya está registrado.");

        var hash = BCrypt.Net.BCrypt.HashPassword(cmd.Password);
        var user = new Domain.Entities.User(cmd.Email, cmd.Name, hash);

        await _users.AddAsync(user, ct);
        await _uow.SaveChangesAsync(ct);

        return new UserResponseDto(user.Id, user.Name, user.Email, user.Role, user.CreatedAt);
    }
}

public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
{
    private readonly IUserRepository _users;
    private readonly ITokenService   _tokenService;

    public LoginUserHandler(IUserRepository users, ITokenService tokenService)
    {
        _users        = users;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> Handle(LoginUserCommand cmd, CancellationToken ct)
    {
        var user = await _users.GetByEmailAsync(cmd.Email, ct)
            ?? throw new BusinessRuleException("Credenciales inválidas.");

        if (!BCrypt.Net.BCrypt.Verify(cmd.Password, user.PasswordHash))
            throw new BusinessRuleException("Credenciales inválidas.");

        var token = _tokenService.GenerateToken(user);
        return new AuthResponseDto(user.Id, user.Name, user.Email, token);
    }
}