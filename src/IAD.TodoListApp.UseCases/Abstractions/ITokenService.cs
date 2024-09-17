using IAD.TodoListApp.Core;
using IAD.TodoListApp.Core.Abstractions;

namespace IAD.TodoListApp.UseCases.Abstractions;

/// <summary>
/// Сервис для работы с токенами.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Генерация токена доступа.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    /// <returns> Access токен </returns>
    public Token GenerateToken(UserBase user);
}
