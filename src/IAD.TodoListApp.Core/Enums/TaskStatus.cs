using System.Text.Json.Serialization;

namespace IAD.TodoListApp.Core.Enums;

/// <summary>
/// Статус задачи
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaskStatus
{
    /// <summary>
    /// Ожидает выполнения.
    /// </summary>
    Pending,

    /// <summary>
    /// В процессе выполнения.
    /// </summary
    InProgress,

    /// <summary>
    /// Заверешена.
    /// </summary>
    Completed
}