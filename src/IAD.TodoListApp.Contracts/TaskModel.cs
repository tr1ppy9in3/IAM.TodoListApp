
namespace IAD.TodoListApp.Contracts;

public class TaskModel
{
    /// <summary>
    /// Уникальный идентификатор задачи.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Заголовок задачи.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание задачи.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Дата и время, к которой задача должна быть завершена.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Приоритет задачи.
    /// </summary>
    public string Priority { get; set; }

    /// <summary>
    /// Статус задачи.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Идентификатор категории задачи.
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Название категории задачи.
    /// </summary>
    public string CategoryName { get; set; }


}
