namespace IAD.TodoListApp.UseCases.TodoTask;

using TodoTask = Core.TodoTask;

/// <summary>
/// Репозиторий для доступа к задачам.
/// </summary>
public interface ITaskRepository
{
    /// <summary>
    /// Получение всех доступных пользователю задач.
    /// </summary>
    /// <param name="UserId"> Идентификатор пользователя. </param>
    /// <returns> Список задач. </returns>
    IAsyncEnumerable<TodoTask> GetAllAvailable(long userId);

    /// <summary>
    /// Получить задачу по идентификатору.
    /// </summary>
    /// <param name="Id"> Идентификатор задачи. </param>
    /// <returns> Задачу. </returns>
    Task<TodoTask?> GetById(long id);

    /// <summary>
    /// Добавить задачу.
    /// </summary>
    /// <param name="task"> Задача для добавления. </param>
    Task Add(TodoTask task);

    /// <summary>
    /// Обновить задачу.
    /// </summary>
    /// <param name="task"> Задача для обновления. </param>
    Task Update(TodoTask task);

    /// <summary>
    /// Удалить задачу.
    /// </summary>
    /// <param name="task"> Задача для удаления. </param>
    Task Delete(TodoTask task);

    /// <summary>
    /// Удалить задачу.
    /// </summary>
    /// <param name="id"> ИДентификатор задачи. </param>
    Task Delete(long id);
}
