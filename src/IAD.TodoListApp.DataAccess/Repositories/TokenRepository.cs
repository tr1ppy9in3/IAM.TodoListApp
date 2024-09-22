using Microsoft.EntityFrameworkCore;

using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.Auth;

namespace IAD.TodoListApp.DataAccess.Repositories;

public class TokenRepository(Context context) : ITokenRepository
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Token?> GetTokenByValue(string token)
    {
        return await _context.Tokens.FirstOrDefaultAsync(t => t.Value == token);
    }

    public async Task<bool> ExistsByValue(string token)
    {
        return await _context.Tokens.AnyAsync(t => t.Value == token);
    }

    public async Task<Token> Create(Token token)
    {
        _context.Tokens.Add(token);
        await _context.SaveChangesAsync();
        return token;    
    }

    public async Task Update(Token token)
    {
        _context.Tokens.Update(token);
        await _context.SaveChangesAsync();
    }

    public async Task AddToBlacklist(string token)
    {
        var dbToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Value == token);
        if (dbToken != null)
        {
            dbToken.IsBlacklisted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsBlacklisted(string token)
    {
        return await _context.Tokens.AnyAsync(t => t.Value == token && t.IsBlacklisted);
    }

}
