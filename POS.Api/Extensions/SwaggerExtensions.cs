using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var openApi = new OpenApiInfo
        {
            Title = "POS API",
            Version = "v1",
            Description = "Sistema de Venta Backend API 2026",
            TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
            Contact = new OpenApiContact
            {
                Name = "ECONSER S.A.C.",
                Email = "econser@gmail.com",
                Url = new Uri("https://sirtech.com.pe")
            },
            License = new OpenApiLicense
            {
                Name = "Use under LICX",
                Url = new Uri("https://opensource.org/licenses/")
            }
        };

        services.AddSwaggerGen(x =>
        {
            openApi.Version = "v1";
            x.SwaggerDoc("v1", openApi);

            var securitySchema = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "JWT Bearer Token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            x.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securitySchema, new string[] { } }
            });
        });

        return services;
    
    }
}