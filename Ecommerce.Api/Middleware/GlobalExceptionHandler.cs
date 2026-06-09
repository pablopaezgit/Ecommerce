using ECommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Error no controlado: {Message}", exception.Message);

        var (status, title) = exception switch
        {
            NotFoundException     e => (StatusCodes.Status404NotFound,           e.Message),
            BusinessRuleException e => (StatusCodes.Status400BadRequest,         e.Message),
            DomainException       e => (StatusCodes.Status422UnprocessableEntity, e.Message),
            _                       => (StatusCodes.Status500InternalServerError, "Ocurrió un error interno.")
        };

        var problem = new ProblemDetails
        {
            Status   = status,
            Title    = title,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}