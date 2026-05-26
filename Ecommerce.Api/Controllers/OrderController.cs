// Api/Controllers/OrderController.cs
using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // todas las rutas de orders requieren estar autenticado
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service) => _service = service;

    // GET api/order/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await _service.GetByIdAsync(id, ct));

    // GET api/order/user/{userId}
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId, CancellationToken ct)
        => Ok(await _service.GetByUserIdAsync(userId, ct));

    // POST api/order
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
