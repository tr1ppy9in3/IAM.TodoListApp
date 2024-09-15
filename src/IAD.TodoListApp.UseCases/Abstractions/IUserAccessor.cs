using IAD.TodoListApp.Core.Enums;

namespace IAD.TodoListApp.UseCases.Abstractions;

/// <summary>
/// Интерфейс для взаимодействия с аунтефицированным пользователем.
/// </summary>
public interface IUserAccessor
{
    /// <summary>
    /// Получить индетификатор пользователя.
    /// </summary>
    /// <returns> Индетификатор. </returns>
    public long GetUserId();

    /// <summary>
    /// Получить логин пользователя.
    /// </summary>
    /// <returns> Логин пользователя. </returns>
    public string GetUsername();

    /// <summary>
    /// Получить роль пользователя.
    /// </summary>
    /// <returns> Роль пользователя. </returns>
    public UserRole GetRole();
}

