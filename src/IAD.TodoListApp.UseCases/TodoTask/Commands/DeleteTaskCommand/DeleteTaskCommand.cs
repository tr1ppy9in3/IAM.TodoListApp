using MediatR;
using FluentValidation;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TodoTask.Validators;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.DeleteTaskCommand;

/// <summary>
/// Команда для удаления задачи.
/// </summary>
/// <param name="TaskId"> Идентификатор задачи. </param>
public record DeleteTaskCommand(long TaskId) : IValidatableCommand<Unit>;

/// <summary>
/// Валидатор для команды по удалению задачи.
/// </summary>
public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator(ITaskRepository taskRepository)
    {
        RuleFor(x => x.TaskId)
            .NotNull().WithMessage("TaskId is required!")
            .SetValidator(new TaskExistsValidator(taskRepository));
    }
}