namespace IAD.TodoListApp.Core;

/// <summary>
/// Модель токена
/// </summary>
public class Token
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Значение токена (JWT)
    /// </summary>
    public required string Value { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public required string Username { get; set; }
    
    /// <summary>
    /// Время истечения.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Время создания.
    /// </summary>
    public DateTime CreatedAt = DateTime.UtcNow;

    /// <summary>
    /// Занесен ли токен в черный список.
    /// </summary>
    public bool IsBlacklisted { get; set; } = false;
}
