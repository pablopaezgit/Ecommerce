using ECommerce.Application.Commands.Auth;
using ECommerce.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new RegisterUserCommand(dto.Name, dto.Email, dto.Password), ct);
        return CreatedAtAction(nameof(Register), result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new LoginUserCommand(dto.Email, dto.Password), ct);
        return Ok(result);
    }
}
