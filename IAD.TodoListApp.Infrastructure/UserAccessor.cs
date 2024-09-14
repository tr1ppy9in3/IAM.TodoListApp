using Microsoft.AspNetCore.Http;
using System.Security.Claims;

using IAD.TodoListApp.Core.Enums;
using IAD.TodoListApp.UseCases.Abstractions;

namespace IAD.TodoListApp.Infrastructure;

/// <summary>
/// Сервис для доступа к данным пользователя.
/// </summary>
public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    /// <summary>
    /// Авторизированный пользователь.
    /// </summary>
    private readonly ClaimsPrincipal _user = httpContextAccessor.HttpContext?.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    /// <summary>
    /// Получить идентификатор пользователя.
    /// </summary>
    /// <returns> Идентификатор пользователя. </returns>
    public long GetUserId()
    {
        if (long.TryParse(_user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out long id))
        {
            return id;
        }

        return -1;
    }

    /// <summary>
    /// Получить юзернейм пользователя.
    /// </summary>
    /// <returns> Юзернейм пользователя. </returns>
    public string GetUsername()
    {
        return _user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Получить роль пользователя.
    /// </summary>
    /// <returns> Роль пользователя. </returns>
    public UserRole GetRole()
    {
        var roleClaim = _user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        return Enum.TryParse<UserRole>(roleClaim, out var role) ? role : UserRole.None;
    }
}