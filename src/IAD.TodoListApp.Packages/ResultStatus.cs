namespace IAD.TodoListApp.Packages;

/// <summary>
/// Статусы выполнения команд и запросов
/// </summary>
public enum ResultStatus
{
    /// <summary>
    /// Статус не установлен
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Выполнено успешно
    /// </summary>
    Ok = 200,
    /// <summary>
    /// Выполнено успешно и создан новый ресурс
    /// </summary>
    Created = 201,
    /// <summary>
    /// Выполнено успешно. Возвращать результат не требуется
    /// </summary>
    NoContent = 204,
    /// <summary>
    /// Некорректные параметры команды/запроса
    /// </summary>
    Invalid = 400,
    /// <summary>
    /// Действие недоступно
    /// </summary>
    Forbidden = 403,
    /// <summary>
    /// Объект для создания конфликтует с уже существующими объектами
    /// </summary>
    Conflict = 409,
    /// <summary>
    /// Ошибка выполнения
    /// </summary>
    Error = 422,
}