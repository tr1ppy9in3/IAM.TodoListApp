namespace IAD.TodoListApp.Auth.Core;

/// <summary>
/// Модель токена
/// </summary>
/// <param name="Value"> Строка токена для доступа</param>
public record class Token(string Value);
