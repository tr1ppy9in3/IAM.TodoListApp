using System.Text.Json.Serialization;

namespace IAD.TodoListApp.Core.Enums;

/// <summary>
/// Роли пользователя.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    /// <summary>
    /// Стандартное значение.
    /// Без роли.
    /// </summary>
    None,

    /// <summary>
    /// Обычный пользователь.
    /// </summary>
    RegularUser,

    /// <summary>
    /// Админ.
    /// </summary>
    Admin
}

