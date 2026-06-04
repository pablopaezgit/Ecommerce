using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Commands.Auth;

public record RegisterUserCommand(
    string Name,
    string Email,
    string Password) : IRequest<UserResponseDto>;

public record LoginUserCommand(
    string Email,
    string Password) : IRequest<AuthResponseDto>;
