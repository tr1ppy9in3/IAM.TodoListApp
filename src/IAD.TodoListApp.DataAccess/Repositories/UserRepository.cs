using Microsoft.EntityFrameworkCore;

using IAD.TodoListApp.DataAccess;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;
using IAD.TodoListApp.Core.Abstractions;

namespace RIP.TodoList.DataAccess.Repositories;

using PasswordOptions = IAD.TodoListApp.Core.Options.PasswordOptions;

/// <summary>
/// Реализация <see cref="IUserRepository"/>.
/// </summary>
public class UserRepository(Context context) : IUserRepository
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc/>
    public IAsyncEnumerable<UserBase> GetAll()
    {
        return _context.Users
               .AsNoTracking()
               .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public async Task<UserBase?> Resolve(string login, string password)
    {
        return await _context.Users
                             .AsNoTracking()
                             .FirstOrDefaultAsync(u => u.Login == login && u.PasswordHash == password);

    }

    /// <inheritdoc/>
    public async Task<UserBase?> GetById(long id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <inheritdoc/>
    public async Task<UserBase?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <inheritdoc/>
    public async Task<UserBase?> GetByLogin(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
    }

    /// <inheritdoc/>
    public async Task ChangeEmail(UserBase user, string email)
    {
        user.Email = email;
        await _context.SaveChangesAsync();

    }

    /// <inheritdoc/>
    public async Task ChangePassword(UserBase user, string password, PasswordOptions passwordOptions)
    {
        user.SetPassword(password, passwordOptions);
        await _context.SaveChangesAsync();

    }

    /// <inheritdoc/>
    public async Task Add(UserBase user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Update(UserBase user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(UserBase user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

    }

}
