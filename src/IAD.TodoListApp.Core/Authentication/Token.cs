namespace RIP.TodoList.Core.Authentication;

/// <summary>
/// Модель токена
/// </summary>
/// <param name="Value"> Строка токена для доступа</param>
public record class Token(string Value);
