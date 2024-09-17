namespace IAM.TodoListApp.Auth.Core;

/// <summary>
/// Настройки JWT токена
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Ключ
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Издатель
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Получатель
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Время истечения (в минутах)
    /// </summary>
    public int ExpiryMinutes { get; set; }
}