using MediatR;
using FluentValidation;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TodoTask.Models;
using IAD.TodoListApp.UseCases.TaskCategory;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.UpdateTaskCommand;

/// <summary>
/// Команда обновления задачи.
/// </summary>
/// <param name="TaskId"> Идентификатор задачи.</param>
/// <param name="UserId"> Идентификатор пользователя.</param>
/// <param name="Model"> Модель задачи.</param>
public record UpdateTaskCommand(long TaskId, long UserId, TaskInputModel Model) : IRequest<Result<Unit>>;

/// <summary>
/// Валидатор команды обновления задачи.
/// </summary>
public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator(ITaskCategoryRepository taskCategoryRepository)
    {
        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Task model is required!")
            .SetValidator(new TaskInputModelValidator(taskCategoryRepository));
    }
}
