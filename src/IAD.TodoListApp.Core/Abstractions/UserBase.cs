using IAD.TodoListApp.Core.Enums;
using IAD.TodoListApp.Core.Options;
using IAD.TodoListApp.Core.Services;

namespace IAD.TodoListApp.Core.Abstractions;

/// <summary>
/// Базовый класс пользователя.
/// </summary>
public abstract class UserBase
{
    /// <summary>
    /// Индетификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Хэш пароля
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Роль
    /// </summary>
    public UserRole Role { get; set; } = UserRole.None;

    /// <summary>
    /// Заблокирован (да, нет)
    /// </summary>
    public bool IsBlocked { get; set; } = false;

    /// <summary>
    /// Сеттер пароля
    /// </summary>
    /// <param name="password"> Строка пароля</param>
    /// <param name="passwordOptions"> Параметры пароля</param>
    public void SetPassword(string password, PasswordOptions passwordOptions)
    {
        PasswordHash = CryptographyService.HashPassword(password, passwordOptions.Salt);
    }
}
