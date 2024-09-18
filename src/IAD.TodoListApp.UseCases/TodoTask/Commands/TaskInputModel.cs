using IAD.TodoListApp.Core.Enums;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands;

/// <summary>
/// Входная модель задачи.
/// </summary>
public class TaskInputModel
{
    /// <summary>
    /// Заголовок задачи.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Описание задачи.
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
    /// Идентификатор категории задачи.
    /// </summary>
    public long? CategoryId { get; set; }
}
