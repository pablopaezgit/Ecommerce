namespace ECommerce.Application.DTOs;

// Registro de nuevo usuario
public record RegisterUserDto(
    string Name,
    string Email,
    string Password);

// Login
public record LoginDto(
    string Email,
    string Password);

// Respuesta con token JWT
public record AuthResponseDto(
    Guid   UserId,
    string Name,
    string Email,
    string Token);

// Datos públicos del usuario (sin contraseña)
public record UserResponseDto(
    Guid     Id,
    string   Name,
    string   Email,
    string   Role,
    DateTime CreatedAt);
