using IAD.TodoListApp.Core;

namespace IAD.TodoListApp.UseCases.Auth;

/// <summary>
/// Репозиторий для взаимодействия с токена.
/// </summary>
public interface ITokenRepository
{
    /// <summary>
    /// Получить токен по значению.
    /// </summary>
    /// <param name="token"> Значение токена. </param>
    /// <returns> Токен. </returns>
    Task<Token?> GetTokenByValue(string token);

    /// <summary>
    /// Существует ли токен по значению.
    /// </summary>
    /// <param name="token"> Значение токена. </param>
    /// <returns> True | False </returns>
    Task<bool> ExistsByValue(string  token);

    /// <summary>
    /// Создать токен.
    /// </summary>
    /// <param name="token"> Токен. </param>
    /// <returns> Токен. </returns>
    Task<Token> Create(Token token);

    /// <summary>
    /// Обновить токен.
    /// </summary>
    /// <param name="token"> Токен. </param>
    Task Update(Token token);

    /// <summary>
    /// Добавить токен в черный список.
    /// </summary>
    /// <param name="token">Значение токена.</param>
    Task AddToBlacklist(string token);

    /// <summary>
    /// Проверить находиться ли токен в черном списке.
    /// </summary>
    /// <param name="token">Значение токена. </param>
    /// <returns> True | False </returns>
    Task<bool> IsBlacklisted(string token);
}
