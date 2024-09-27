using MediatR;
using FluentValidation;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.TaskCategory.Commands;

/// <summary>
/// Команда удаления категории задачи.
/// </summary>
/// <param name="TaskCategoryId"> Идентификатор категории. </param>
public record class DeleteTaskCategoryCommand(long TaskCategoryId) : IRequest<Result<Unit>>;

/// <summary>
/// Обработчик команды удаления категории задачи.
/// </summary>
public class DeleteTaskCategoryCommandHandler() : IRequestHandler<DeleteTaskCategoryCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(DeleteTaskCategoryCommand request, CancellationToken _)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Валидатор команды удаления категории задачи.
/// </summary>
public class DeleteTaskCategoryCommandValidator : AbstractValidator<DeleteTaskCategoryCommand>
{
    public DeleteTaskCategoryCommandValidator() 
    {

    }
}