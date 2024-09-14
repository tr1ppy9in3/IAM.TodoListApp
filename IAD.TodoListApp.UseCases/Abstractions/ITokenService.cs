using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Authentication;

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
