
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalApiLivros.Context;

namespace MinimalApiLivros.AppServicesEntensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddApiSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwagger();
        return builder;
    }
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinimalApiLivros", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = @"JWT Authorization header using the Bearer scheme.
                    Enter 'Bearer'[space].Example: \'Bearer 12345abcdef\'",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
        });
        return services;
    }

    public static WebApplicationBuilder AddPersistence(
         this WebApplicationBuilder builder)
    {
    string mySqlConnection =
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new
        Exception("A string de conexão 'DefaultConnection' não foi configurada.");

        builder.Services.AddDbContext<AppDbContext>(
            options => options.UseMySql(mySqlConnection,ServerVersion.AutoDetect(mySqlConnection)));
        
        return builder;
    }
    public static WebApplicationBuilder AddAutenticationJwt(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                        .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddAuthorization();
        return builder;
    }
}
