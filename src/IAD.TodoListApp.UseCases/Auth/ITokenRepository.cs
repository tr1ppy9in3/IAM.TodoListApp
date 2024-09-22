using IAD.TodoListApp.Core;

namespace IAD.TodoListApp.UseCases.Auth;

public interface ITokenRepository
{
    Task<Token?> GetTokenByValue(string token);

    Task<bool> ExistsByValue(string  token);

    Task<Token> Create(Token token);

    Task Update(Token token);

    Task AddToBlacklist(string token);

    Task<bool> IsBlacklisted(string token);
}
