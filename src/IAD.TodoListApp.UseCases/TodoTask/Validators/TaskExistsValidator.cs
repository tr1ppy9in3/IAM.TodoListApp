using FluentValidation;

namespace IAD.TodoListApp.UseCases.TodoTask.Validators;

/// <summary>
/// Валидатор для проверки на существование задачи.
/// </summary>
public class TaskExistsValidator : AbstractValidator<long>
{
    public TaskExistsValidator(ITaskRepository taskRepository)
    {
        RuleFor(taskId => taskId)
            .MustAsync(async (taskId, cancellationToken) =>
            {
                var task = await taskRepository.GetById(taskId);
                return task != null;
            })
            .WithMessage("Task with the specified ID does not exist.");
    }
}
