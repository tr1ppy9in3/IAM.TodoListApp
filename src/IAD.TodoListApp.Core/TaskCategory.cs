using IAD.TodoListApp.Core.Abstractions;

namespace IAD.TodoListApp.Core;

/// <summary>
/// Модель категории задачи.
/// </summary>
public class TaskCategory
{
    /// <summary>
    /// Уникальный идентификатор категории задачи.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Название категории задачи.
    /// Например, "Работа", "Личное", "Проект" и т.д.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание категории задачи.
    /// Дополнительная информация о категории, которая может помочь пользователю понять её назначение.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Пользователь, который создал задачу.
    /// </summary>
    public UserBase? User { get; set; }
    public long UserId { get; set; }
}
