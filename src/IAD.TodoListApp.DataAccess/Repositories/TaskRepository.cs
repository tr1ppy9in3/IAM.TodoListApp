using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;


namespace IAD.TodoListApp.DataAccess.Repositories;

/// <summary>
/// Реализация <see cref="ITaskRepository"/>.
/// </summary>
public class TaskRepository(Context context) : ITaskRepository
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Получить все доступные задачи для пользователя.
    /// </summary>
    /// <param name="UserId">Идентификатор пользователя.</param>
    /// <returns>Перечисление доступных задач.</returns>
    public async IAsyncEnumerable<TodoTask> GetAllAvailable(long userId)
    {
        await foreach (var task in _context.TodoTasks
            .Where(t => t.UserId == userId)
            .AsAsyncEnumerable())
        {
            yield return task;
        }
    }

    /// <summary>
    /// Получить задачу по идентификатору.
    /// </summary>
    /// <param name="Id">Идентификатор задачи.</param>
    /// <returns>Задача.</returns>
    public async Task<TodoTask?> GetById(long id)
    {
        return await _context.TodoTasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <summary>
    /// Добавить новую задачу.
    /// </summary>
    /// <param name="task">Задача для добавления.</param>
    public async Task Add(TodoTask task)
    {
        await _context.TodoTasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить существующую задачу.
    /// </summary>
    /// <param name="task">Задача для обновления.</param>
    public async Task Update(TodoTask task)
    {
        _context.TodoTasks.Update(task);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить задачу по идентификатору.
    /// </summary>
    /// <param name="Id">Идентификатор задачи.</param>
    public async Task Delete(TodoTask task)
    {
        _context.TodoTasks.Remove(task);
        await context.SaveChangesAsync();
    }
}
