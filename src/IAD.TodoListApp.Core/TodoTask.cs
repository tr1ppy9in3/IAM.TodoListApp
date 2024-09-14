using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Enums;

namespace IAD.TodoListApp.Core;

using TaskStatus = Enums.TaskStatus;

/// <summary>
/// Модель задачи.
/// </summary>
public class TodoTask
{
    /// <summary>
    /// Уникальный идентификатор задачи.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Заголовок задачи.
    /// Краткое описание сути задачи, которое обязательно для заполнения.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Подробное описание задачи.
    /// Дополнительная информация о том, что необходимо выполнить.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Дата и время, к которой задача должна быть завершена.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Приоритет задачи.
    /// </summary>
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    /// <summary>
    /// Статус задачи.
    /// </summary>
    public TaskStatus Status { get; set; } = TaskStatus.Pending;

    /// <summary>
    /// Пользователь, который создал задачу.
    /// </summary>
    public UserBase? User { get; set; }
    public long UserId { get; set; }

    /// <summary>
    /// Категория, к которой принадлежит задача.
    /// </summary>
    public TaskCategory? Category { get; set; }
    public long CategoryId { get; set; }
}
