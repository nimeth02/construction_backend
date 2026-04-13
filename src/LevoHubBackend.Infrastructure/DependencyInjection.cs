using LevoHubBackend.Application.Common.Authorization;
using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using LevoHubBackend.Infrastructure.Data;
using LevoHubBackend.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LevoHubBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JwtSettings:Secret is not configured.");

        // --- Database ---
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // --- ASP.NET Core Identity ---
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // --- JWT Bearer Authentication ---
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false; // Set to false for local development (http)
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger("JwtBearer");
                    logger.LogError(context.Exception, "Authentication failed: {Message}", context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnForbidden = context =>
                {
                    var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger("JwtBearer");
                    logger.LogWarning("Authorization failed (Forbidden) for user: {User}", context.HttpContext.User.Identity?.Name);
                    return Task.CompletedTask;
                }
            };
        });

        // --- Authorization Policies ---
        // Each Permission constant maps to a policy that checks the "Permission" claim in the JWT.
        services.AddAuthorization(options =>
        {
            // Templates
            options.AddPolicy(Permissions.Templates.View,   p => p.RequireClaim("Permission", Permissions.Templates.View));
            options.AddPolicy(Permissions.Templates.Create, p => p.RequireClaim("Permission", Permissions.Templates.Create));
            options.AddPolicy(Permissions.Templates.Edit,   p => p.RequireClaim("Permission", Permissions.Templates.Edit));
            options.AddPolicy(Permissions.Templates.Delete, p => p.RequireClaim("Permission", Permissions.Templates.Delete));

            // Departments
            options.AddPolicy(Permissions.Departments.View,   p => p.RequireClaim("Permission", Permissions.Departments.View));
            options.AddPolicy(Permissions.Departments.Create, p => p.RequireClaim("Permission", Permissions.Departments.Create));
            options.AddPolicy(Permissions.Departments.Edit,   p => p.RequireClaim("Permission", Permissions.Departments.Edit));
            options.AddPolicy(Permissions.Departments.Delete, p => p.RequireClaim("Permission", Permissions.Departments.Delete));

            // Users
            options.AddPolicy(Permissions.Users.View,   p => p.RequireClaim("Permission", Permissions.Users.View));
            options.AddPolicy(Permissions.Users.Create, p => p.RequireClaim("Permission", Permissions.Users.Create));
            options.AddPolicy(Permissions.Users.Edit,   p => p.RequireClaim("Permission", Permissions.Users.Edit));
            options.AddPolicy(Permissions.Users.Delete, p => p.RequireClaim("Permission", Permissions.Users.Delete));

            // Stages
            options.AddPolicy(Permissions.Stages.View,   p => p.RequireClaim("Permission", Permissions.Stages.View));
            options.AddPolicy(Permissions.Stages.Create, p => p.RequireClaim("Permission", Permissions.Stages.Create));
            options.AddPolicy(Permissions.Stages.Edit,   p => p.RequireClaim("Permission", Permissions.Stages.Edit));
            options.AddPolicy(Permissions.Stages.Delete, p => p.RequireClaim("Permission", Permissions.Stages.Delete));
        });

        // --- Services ---
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
