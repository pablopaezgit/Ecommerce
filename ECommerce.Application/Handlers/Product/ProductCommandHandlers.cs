using ECommerce.Application.Commands.Product;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Handlers.Product;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
{
    private readonly IProductRepository _products;
    private readonly IUnitOfWork        _uow;

    public CreateProductHandler(IProductRepository products, IUnitOfWork uow)
    {
        _products = products;
        _uow      = uow;
    }

    public async Task<ProductResponseDto> Handle(CreateProductCommand cmd, CancellationToken ct)
    {
        var product = new Domain.Entities.Product(
            cmd.Name, cmd.Description, cmd.Price, cmd.Stock, cmd.CategoryId);

        await _products.AddAsync(product, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(product);
    }

    private static ProductResponseDto MapToDto(Domain.Entities.Product p) =>
        new(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.CreatedAt);
}

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _products;
    private readonly IUnitOfWork        _uow;

    public UpdateProductHandler(IProductRepository products, IUnitOfWork uow)
    {
        _products = products;
        _uow      = uow;
    }

    public async Task Handle(UpdateProductCommand cmd, CancellationToken ct)
    {
        var product = await _products.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException(nameof(Domain.Entities.Product), cmd.Id);

        product.UpdatePrice(cmd.Price);

        await _products.UpdateAsync(product, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _products;
    private readonly IUnitOfWork        _uow;

    public DeleteProductHandler(IProductRepository products, IUnitOfWork uow)
    {
        _products = products;
        _uow      = uow;
    }

    public async Task Handle(DeleteProductCommand cmd, CancellationToken ct)
    {
        var product = await _products.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException(nameof(Domain.Entities.Product), cmd.Id);

        await _products.DeleteAsync(product.Id, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
