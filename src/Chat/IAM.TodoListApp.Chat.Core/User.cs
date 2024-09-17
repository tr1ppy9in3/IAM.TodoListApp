using System.Security.Claims;

namespace IAD.TodoListApp.Chat.Core;

public class User
{
    public string Username { get; set; }

    public User(ClaimsPrincipal principal)
    {
        Username = principal.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous";
    }
}
