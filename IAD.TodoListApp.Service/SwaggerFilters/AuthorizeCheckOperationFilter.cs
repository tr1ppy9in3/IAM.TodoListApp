using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IAD.TodoListApp.Service.SwaggerFilters;

/// <summary>
/// Фильтр авторизации для сваггера
/// </summary>
public class AuthorizeCheckOperationFilter : IOperationFilter
{
    /// <summary>
    /// Применение
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var controllerAuthAttributes = context.MethodInfo.DeclaringType!
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Distinct();

        var methodAuthAttributes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Distinct();

        var authAttributes = controllerAuthAttributes.Union(methodAuthAttributes).Distinct();

        if (authAttributes.Any())
        {
            var roles = authAttributes
                .Where(attr => !string.IsNullOrWhiteSpace(attr.Roles))
                .Select(attr => attr.Roles)
                .Distinct()
                .ToList();

            var rolesDescription = string.Join(", ", roles);

            operation.Responses.Add("401", new OpenApiResponse { Description = "Неавторизован" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Отказано в доступе" });

            var securityRequirement = new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            };

            operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };

            if (!string.IsNullOrEmpty(rolesDescription))
            {
                operation.Description += $" (Roles: {rolesDescription})";
            }
        }
    }
}