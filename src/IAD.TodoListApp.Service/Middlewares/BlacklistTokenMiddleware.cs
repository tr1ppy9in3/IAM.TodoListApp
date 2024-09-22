using IAD.TodoListApp.UseCases.Auth;

namespace IAD.TodoListApp.Service.Middlewares;

/// <summary>
/// Пайплайн для проверки существования токена в черном листе.
/// </summary>
public class BlacklistTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public BlacklistTokenMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Метод пайплайна.
    /// </summary>
    /// <param name="context"> Контекст запроса. </param>

    public async Task Invoke(HttpContext context)
    {
        using var scope = _serviceProvider.CreateScope();
        var tokenRepository = scope.ServiceProvider.GetRequiredService<ITokenRepository>();

        var authorizationHeader = context.Request.Headers.Authorization;

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            var token = authorizationHeader.ToString().Split(" ").Last();

            if (!await tokenRepository.ExistsByValue(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("This token has not registred.");
                return;
            }

            if (await tokenRepository.IsBlacklisted(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("This token has been blacklisted.");
                return;
            }
        }

        await _next(context);
    }
}