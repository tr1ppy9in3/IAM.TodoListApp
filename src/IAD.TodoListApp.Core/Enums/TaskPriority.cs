using System.Text.Json.Serialization;

namespace IAD.TodoListApp.Core.Enums;

/// <summary>
/// Приоритет задачи
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaskPriority
{
    /// <summary>
    /// Минимальный.
    /// </summary>
    Low,

    /// <summary>
    /// Средний.
    /// </summary>
    Medium,

    /// <summary>
    /// Наивысвший.
    /// </summary>
    High
}
