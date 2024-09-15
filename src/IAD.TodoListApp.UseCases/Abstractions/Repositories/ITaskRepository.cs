using IAD.TodoListApp.Core;

namespace IAD.TodoListApp.UseCases.Abstractions.Repositories;

/// <summary>
/// Репозиторий для доступа к задачам.
/// </summary>
public interface ITaskRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    IAsyncEnumerable<TodoTask> GetAllAvailable(long userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<TodoTask?> GetById(long id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    Task Add(TodoTask task);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    Task Update(TodoTask task);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task Delete(TodoTask task);
}
