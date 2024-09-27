using MediatR;

namespace IAD.TodoListApp.Packages;

/// <summary>
/// Валидатор команды
/// </summary>
/// <typeparam name="TRequest">Тип запроса</typeparam>
/// <typeparam name="TResponseValue">Тип ответа</typeparam>
public interface IValidationBehavior<TRequest, TResponseValue> : IPipelineBehavior<TRequest, Result<TResponseValue>>
     where TRequest : IRequest<Result<TResponseValue>>
{ }