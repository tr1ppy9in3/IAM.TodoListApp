using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.TaskCategory;
using Microsoft.EntityFrameworkCore;

namespace IAD.TodoListApp.DataAccess.Repositories;

/// <summary>
/// Реализация <see cref="ITaskCategoryRepository"/>.
/// </summary>
public class TaskCategoryRepository(Context context) : ITaskCategoryRepository
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc/>
    public IAsyncEnumerable<TaskCategory> GetAllAvailable(long userId)
    {
        return _context.TaskCategories
                       .Where(tc => tc.UserId  == userId)
                       .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<TaskCategory?> GetById(long id)
    {
        return _context.TaskCategories
                       .FirstOrDefaultAsync(tc => tc.Id == id);
    }

    /// <inheritdoc/>
    public async Task Add(TaskCategory taskCategory)
    {
        _context.TaskCategories.Add(taskCategory);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Update(TaskCategory taskCategory)
    {
        _context.TaskCategories.Update(taskCategory);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(TaskCategory taskCategory)
    {
        _context.TaskCategories.Remove(taskCategory);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(long id)
    {
        var taskCategory = await GetById(id);

        if (taskCategory is not null)
        {
            _context.TaskCategories.Remove(taskCategory);
            await context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task<bool> IsTaskCategoryAvailable(long categoryId, long userId)
    {
        return await _context.TaskCategories.AnyAsync(c => c.Id == categoryId && c.UserId == userId);
    }

    
}
