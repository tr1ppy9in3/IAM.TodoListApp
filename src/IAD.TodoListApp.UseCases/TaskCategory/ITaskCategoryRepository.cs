namespace IAD.TodoListApp.UseCases.TaskCategory;

/// <summary>
/// Репозиторий для доступа к категориям задач.
/// </summary>
public interface ITaskCategoryRepository
{
    /// <summary>
    /// Получить все доступные пользователю категории задач.
    /// </summary>
    /// <returns> Список из категорий задач. </returns>
    IAsyncEnumerable<Core.TaskCategory> GetAllAvailable(long userId);

    /// <summary>
    /// Получить категорию задач по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор задачи. </param>
    /// <returns> Категория задачи. </returns>
    Task<Core.TaskCategory?> GetById(long id);

    /// <summary>
    /// Добавление категории задач.
    /// </summary>
    /// <param name="taskCategory"> Категория задачи для добавления. </param>
    Task Add(Core.TaskCategory taskCategory);
    
    /// <summary>
    /// Обновлении категории задач.
    /// </summary>
    /// <param name="taskCategory"> Категория задачи для обновления. </param>
    Task Update(Core.TaskCategory taskCategory);

    /// <summary>
    /// Удаление категории задач.
    /// </summary>
    /// <param name="taskCategory"> Категория задачи для удаления. </param>
    Task Delete(Core.TaskCategory taskCategory);

    /// <summary>
    /// Удалении категории задач.
    /// </summary>
    /// <param name="id"> Идентификатор категории задачи. </param>
    Task Delete(long id);

    /// <summary>
    /// Проверка принадлежности категории задач к пользователю.
    /// </summary>
    /// <param name="categoryId"> Идентификатор категории задачи. </param> 
    /// <param name="userId"> Идентификатор пользователя. </param>
    Task<bool> IsTaskCategoryAvailable(long categoryId, long userId);
}
