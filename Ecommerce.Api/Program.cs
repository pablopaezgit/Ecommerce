// Api/Program.cs
using ECommerce.Application;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistence;
using Ecommerce.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Servicios ──────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger con soporte para JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name         = "Authorization",
        Type         = SecuritySchemeType.Http,
        Scheme       = "Bearer",
        BearerFormat = "JWT",
        In           = ParameterLocation.Header,
        Description  = "Ingresá el token JWT obtenido en /api/auth/login"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Capas de la aplicación
builder.Services.AddApplication();                          // Services con interfaces
builder.Services.AddInfrastructure(builder.Configuration); // DbContext, Repos, JWT, Auth

// ── Pipeline ───────────────────────────────────────────────────────────────
var app = builder.Build();

// Aplicar migraciones automáticamente al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Orden correcto del middleware:
// 1. Logging — registra TODOS los requests antes de que se procesen
// 2. ErrorHandling — atrapa excepciones del resto del pipeline
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication(); // ← siempre antes de UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
