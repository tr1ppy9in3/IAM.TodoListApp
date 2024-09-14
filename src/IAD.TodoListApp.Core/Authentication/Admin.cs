using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Enums;

namespace IAD.TodoListApp.Core.Authentication;

public class Admin : UserBase
{
    /// <summary>
    /// Роль.
    /// </summary>
    public new UserRole Role { get; set; } = UserRole.Admin;
}
