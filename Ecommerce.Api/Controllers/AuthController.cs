// Api/Controllers/AuthController.cs
using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService    _userService;
    private readonly JwtTokenService _jwtService;

    public AuthController(IUserService userService, JwtTokenService jwtService)
    {
        _userService = userService;
        _jwtService  = jwtService;
    }

    // POST api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto, CancellationToken ct)
    {
        var user = await _userService.RegisterAsync(dto, ct);
        return CreatedAtAction(nameof(Register), user);
    }

    // POST api/auth/login → devuelve token JWT
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
    {
        // ValidateCredentials lanza BusinessRuleException si las credenciales son incorrectas
        // El ErrorHandlingMiddleware la convierte en 400 automáticamente
        var user  = await _userService.ValidateCredentialsAsync(dto, ct);
        var token = _jwtService.GenerateToken(user);

        return Ok(new AuthResponseDto(user.Id, user.Name, user.Email, token));
    }
}
