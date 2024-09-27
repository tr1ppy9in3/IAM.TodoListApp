using FluentValidation;
using MediatR;

namespace IAD.TodoListApp.Packages;

/// <summary>
/// Интеграция валидации в пайплайн обработки команд
/// </summary>
/// <typeparam name="TRequest">Запрос</typeparam>
/// <typeparam name="TResponseValue">Ответ</typeparam>
public class ValidationBehavior<TRequest, TResponseValue> : IValidationBehavior<TRequest, TResponseValue>
    where TRequest : IRequest<Result<TResponseValue>>
{
    private readonly IEnumerable<IValidator> _validators;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="validators">Валидаторы</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    /// <summary>
    /// Выполнить валидацию
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="next">Следующий обработчик</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Результат выполнения команды</returns>
    public async Task<Result<TResponseValue>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponseValue>> next, CancellationToken cancellationToken)
    {
        if (_validators?.Any() ?? false)
        {
            var context = new ValidationContext<TRequest>(request);
            var tasks = _validators.Select(x => x.ValidateAsync(context, cancellationToken));
            var validationResults = await Task.WhenAll(tasks);

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                return Result<TResponseValue>.Invalid(failures.Select(x => x.ErrorMessage).ToArray());
            }
        }
        return await next();
    }
}