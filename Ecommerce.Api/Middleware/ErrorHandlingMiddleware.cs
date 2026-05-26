using System.Net;
using System.Text.Json;
using ECommerce.Domain.Exceptions;

namespace Ecommerce.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next   = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // sigue con el pipeline normal
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Según el tipo de excepción, asignamos el código HTTP correcto
        var (statusCode, message) = ex switch
        {
            NotFoundException      e => (HttpStatusCode.NotFound,           e.Message),
            BusinessRuleException  e => (HttpStatusCode.BadRequest,         e.Message),
            ArgumentException      e => (HttpStatusCode.BadRequest,         e.Message),
            _                        => (HttpStatusCode.InternalServerError, "Ocurrió un error interno.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode  = (int)statusCode;

        var response = JsonSerializer.Serialize(new
        {
            status  = (int)statusCode,
            error   = message
        });

        return context.Response.WriteAsync(response);
    }
}