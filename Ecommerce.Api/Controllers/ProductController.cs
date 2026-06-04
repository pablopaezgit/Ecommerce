using ECommerce.Application.Commands.Product;
using ECommerce.Application.DTOs;
using ECommerce.Application.Queries.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllProductsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await _mediator.Send(new GetProductByIdQuery(id), ct));

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateProductDto dto, CancellationToken ct)
    {
        var cmd     = new CreateProductCommand(dto.Name, dto.Description, dto.Price, dto.Stock, dto.CategoryId);
        var created = await _mediator.Send(cmd, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, UpdateProductDto dto, CancellationToken ct)
    {
        await _mediator.Send(new UpdateProductCommand(id, dto.Name, dto.Description, dto.Price, dto.Stock), ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteProductCommand(id), ct);
        return NoContent();
    }
}