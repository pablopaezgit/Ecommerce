using ECommerce.Application.Commands.Order;
using ECommerce.Application.DTOs;
using ECommerce.Application.Queries.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await _mediator.Send(new GetOrderByIdQuery(id), ct));

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId, CancellationToken ct)
        => Ok(await _mediator.Send(new GetOrdersByUserQuery(userId), ct));

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto, CancellationToken ct)
    {
        var cmd     = new CreateOrderCommand(dto.UserId, dto.Items);
        var created = await _mediator.Send(cmd, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
