using Microsoft.EntityFrameworkCore;
using IAD.TodoListApp.UseCases.TaskCategory;

namespace IAD.TodoListApp.DataAccess.TaskCategory;

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
    public IAsyncEnumerable<Core.TaskCategory> GetAllAvailable(long userId)
    {
        return _context.TaskCategories
                       .Where(tc => tc.UserId == userId)
                       .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<Core.TaskCategory?> GetById(long id)
    {
        return _context.TaskCategories.FirstOrDefaultAsync(tc => tc.Id == id);
    }

    /// <inheritdoc/>
    public async Task Add(Core.TaskCategory taskCategory)
    {
        await _context.TaskCategories.AddAsync(taskCategory);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Update(Core.TaskCategory taskCategory)
    {
        _context.TaskCategories.Update(taskCategory);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(Core.TaskCategory taskCategory)
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

    /// <inheritdoc/>
    public async Task<Core.TaskCategory?> GetByName(string name)
    {
        return await _context.TaskCategories
            .FirstOrDefaultAsync(tc => tc.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
