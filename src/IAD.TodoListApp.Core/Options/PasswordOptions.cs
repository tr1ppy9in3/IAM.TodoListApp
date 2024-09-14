namespace IAD.TodoListApp.Core.Options;

/// <summary>
/// Параметры пароля.
/// </summary>
public class PasswordOptions
{
    /// <summary>
    /// Соль для шифрования пароля
    /// </summary>
    public string Salt { get; set; } = string.Empty;
}