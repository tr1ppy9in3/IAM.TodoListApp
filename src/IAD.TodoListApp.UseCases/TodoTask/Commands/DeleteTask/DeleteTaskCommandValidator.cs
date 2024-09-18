using FluentValidation;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.DeleteTask;

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
