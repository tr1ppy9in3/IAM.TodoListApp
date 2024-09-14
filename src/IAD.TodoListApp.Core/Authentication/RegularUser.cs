using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Enums;

namespace IAD.TodoListApp.Core.Authentication;

public class RegularUser : UserBase
{
    /// <summary>
    /// Фотография
    /// </summary>
    public byte[]? ProfilePic { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; } = string.Empty;

    /// <summary>
    /// Роль.
    /// </summary>
    public new UserRole Role { get; set; } = UserRole.RegularUser;
}
