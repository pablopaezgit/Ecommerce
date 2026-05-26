// Application/ApplicationServiceRegistration.cs
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registramos por interfaz → implementación (patrón correcto de DI)
        services.AddScoped<IProductService,  ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserService,     UserService>();
        services.AddScoped<IOrderService,    OrderService>();

        return services;
    }
}
