namespace RIP.TodoList.Packages;


/// <summary>
/// Результат выполнения команды/запроса
/// </summary>
/// <typeparam name="TResponseValue">Тип значения</typeparam>
public readonly struct Result<TResponseValue>
{
    private const string STATUS_ERROR_MSG = "Статус {0} не подразумевает наличие результата";
    private const string VALUE_ERROR_MSG = "Значение результата не задано";

    #region factories
    /// <summary>
    /// Успешное выполнение команды
    /// </summary>
    /// <param name="value">Полезная нагрузка</param>
    /// <returns>Результат выполнения команды</returns>
    public static Result<TResponseValue> Success(TResponseValue value) => new(value);
    /// <summary>
    /// Успешно создан новый ресурс
    /// </summary>
    /// <param name="value">Полезная нагрузка</param>
    /// <returns>Результат выполнения команды</returns>
    public static Result<TResponseValue> SuccessfullyCreated(TResponseValue value) => new(value, ResultStatus.Created);
    /// <summary>
    /// Не удалось выполнить команду
    /// </summary>
    /// <param name="errors">Список ошибок</param>
    /// <returns>Результат выполнения команды</returns>
    public static Result<TResponseValue> Error(IReadOnlyCollection<string> errors) => new(errors, ResultStatus.Error);
    /// <summary>
    /// Не удалось выполнить команду
    /// </summary>
    /// <param name="error">Список ошибок</param>
    /// <returns>Результат выполнения команды</returns>
    public static Result<TResponseValue> Error(string error) => new(new List<string> { error }, ResultStatus.Error);
    /// <summary>
    /// Невозможно выполнить команду с заданными параметрами
    /// </summary>
    /// <param name="errors">Список ошибок</param>
    /// <returns>Результат выполнения команды</returns>
    public static Result<TResponseValue> Invalid(IReadOnlyCollection<string> errors) => new(errors, ResultStatus.Invalid);
    /// <summary>
    /// Невозможно выполнить команду с заданными параметрами
    /// </summary>
    /// <param name="error">Список ошибок</param>
    /// <returns>Результат выполнения команды</returns>
    public static Result<TResponseValue> Invalid(string error) => new(new List<string> { error }, ResultStatus.Invalid);
    /// <summary>
    /// Объект для создания конфликтует с уже существующими объектами
    /// </summary>
    /// <param name="error"> Ошибка. </param>
    /// <returns> Результат выполнения команды. </returns>
    public static Result<TResponseValue> Conflict(string error) => new(new List<string> { error }, ResultStatus.Conflict);
    /// <summary>
    /// Объект для создания конфликтует с уже существующими объектами
    /// </summary>
    /// <param name="error"> Список ошибок. </param>
    /// <returns> Результат выполнения команды. </returns>
    public static Result<TResponseValue> Conflict(IReadOnlyCollection<string> errors) => new(errors, ResultStatus.Conflict);
    /// <summary>
    /// Не пройдена проверка доступа
    /// </summary>
    /// <returns>Результат выполнения команды</returns>
    public static Result<TResponseValue> Forbidden() => new(ResultStatus.Forbidden);
    /// <summary>
    /// Успешное выполнение, возврат результата не требуется
    /// </summary>
    /// <returns>Результат выполнения команды</returns>
    public static Result<TResponseValue> Empty() => new(ResultStatus.NoContent);
    #endregion

    private Result(TResponseValue value, ResultStatus status = ResultStatus.Ok)
    {
        this.value = value;
        Status = status;
    }

    private Result(IReadOnlyCollection<string> errors, ResultStatus status)
    {
        Status = status;
        Errors = errors;
    }

    private Result(ResultStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// Статус
    /// </summary>
    public ResultStatus Status { get; } = ResultStatus.Unknown;

    /// <summary>
    /// Успешное выполнение
    /// </summary>
    public bool IsSuccess => Status.IsSuccess();

    /// <summary>
    /// Ошибки
    /// </summary>
    public IReadOnlyCollection<string>? Errors { get; } = null;

    /// <summary>
    /// Значение результата.
    /// </summary>
    private readonly TResponseValue? value = default;

    /// <summary>
    /// Получить значение
    /// </summary>
    /// <returns>Значение</returns>
    /// <exception cref="AppException">
    /// Статус результата не подразумевает наличие результата.
    /// Либо значение отсутствует.
    /// </exception>
    public TResponseValue GetValue() => Status.IsSuccess() && value is not null
            ? value
            : throw new AppException(
                !Status.IsSuccess()
                ? string.Format(STATUS_ERROR_MSG, Status)
                : VALUE_ERROR_MSG);
}