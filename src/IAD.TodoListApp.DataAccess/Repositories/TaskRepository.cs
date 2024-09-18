using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.TodoTask;
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

    /// <inheritdoc/>
    public async IAsyncEnumerable<TodoTask> GetAllAvailable(long userId)
    {
        await foreach (var task in _context.TodoTasks
            .Where(t => t.UserId == userId)
            .AsAsyncEnumerable())
        {
            yield return task;
        }
    }

    /// <inheritdoc/>
    public async Task<TodoTask?> GetById(long id)
    {
        return await _context.TodoTasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <inheritdoc/>
    public async Task Add(TodoTask task)
    {
        await _context.TodoTasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Update(TodoTask task)
    {
        _context.TodoTasks.Update(task);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(TodoTask task)
    {
        _context.TodoTasks.Remove(task);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(long id)
    {
        var task = _context.TodoTasks.Find(id);

        if (task is not null)
        {
            _context.TodoTasks.Remove(task);
            await context.SaveChangesAsync();
        }
    }
}
