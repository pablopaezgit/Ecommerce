using ECommerce.Application.Commands.Category;
using ECommerce.Application.DTOs;
using ECommerce.Application.Queries.Category;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllCategoriesQuery(), ct));

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateCategoryDto dto, CancellationToken ct)
    {
        var created = await _mediator.Send(new CreateCategoryCommand(dto.Name, dto.Description), ct);
        return Created(string.Empty, created);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), ct);
        return NoContent();
    }
}