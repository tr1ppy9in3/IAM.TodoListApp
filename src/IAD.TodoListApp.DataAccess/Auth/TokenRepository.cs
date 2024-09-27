using Microsoft.EntityFrameworkCore;

using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.Auth;

namespace IAD.TodoListApp.DataAccess.Auth;

/// <summary>
/// Реализация <see cref="ITokenRepository"/>.
/// </summary>
public class TokenRepository(Context context) : ITokenRepository
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc/>
    public async Task<Token?> GetTokenByValue(string token)
    {
        return await _context.Tokens.FirstOrDefaultAsync(t => t.Value == token);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsByValue(string token)
    {
        return await _context.Tokens.AnyAsync(t => t.Value == token);
    }

    /// <inheritdoc/>
    public async Task<Token> Create(Token token)
    {
        await _context.Tokens.AddAsync(token);
        await _context.SaveChangesAsync();
        return token;
    }

    /// <inheritdoc/>
    public async Task Update(Token token)
    {
        _context.Tokens.Update(token);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task AddToBlacklist(string token)
    {
        var dbToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Value == token);
        if (dbToken != null)
        {
            dbToken.IsBlacklisted = true;
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task<bool> IsBlacklisted(string token)
    {
        return await _context.Tokens.AnyAsync(t => t.Value == token && t.IsBlacklisted);
    }

}
