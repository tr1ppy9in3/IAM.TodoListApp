using MediatR;

namespace IAD.TodoListApp.Packages;

/// <summary>
/// Команда с валидацией
/// </summary>
/// <typeparam name="TResponseValue">Тип ответа</typeparam>
public interface IValidatableCommand<TResponseValue> : IRequest<Result<TResponseValue>>
{ }