using System.Text;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Base de datos ──────────────────────────────────────────────
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // ApplicationDbContext implementa IUnitOfWork, así que registramos
        // IUnitOfWork resolviendo la misma instancia que ya existe en el scope
        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        // ── Repositorios ───────────────────────────────────────────────
        services.AddScoped<IProductRepository,  ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserRepository,     UserRepository>();
        services.AddScoped<IOrderRepository,    OrderRepository>();

        // ── JWT ────────────────────────────────────────────────────────
        services.AddScoped<JwtTokenService>();

        var jwtSecret = configuration["Jwt:Secret"]
            ?? throw new InvalidOperationException("Jwt:Secret no configurado.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidateAudience         = true,
                    ValidateLifetime         = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer              = configuration["Jwt:Issuer"],
                    ValidAudience            = configuration["Jwt:Audience"],
                    IssuerSigningKey         = new SymmetricSecurityKey(
                                                  Encoding.UTF8.GetBytes(jwtSecret)),
                };
            });

        return services;
    }
}