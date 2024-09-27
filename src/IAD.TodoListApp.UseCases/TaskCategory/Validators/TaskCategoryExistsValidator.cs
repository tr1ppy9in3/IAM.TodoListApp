using FluentValidation;
using IAD.TodoListApp.UseCases.TodoTask;

namespace IAD.TodoListApp.UseCases.TaskCategory.Validators;

/// <summary>
/// Валидатор для проверки на существование категории задач.
/// </summary>
public class TaskCategoryExistsValidator : AbstractValidator<long>
{
    private readonly ITaskCategoryRepository _taskCategoryRepository;

    public TaskCategoryExistsValidator(ITaskCategoryRepository taskCategoryRepository)
    {
        _taskCategoryRepository = taskCategoryRepository
            ?? throw new ArgumentNullException(nameof(taskCategoryRepository));

        RuleFor(taskCategoryId => taskCategoryId)
            .MustAsync(async (taskCategoryId, cancellationToken) =>
            {
                var taskCategory = await _taskCategoryRepository.GetById(taskCategoryId);
                return taskCategory != null;
            })
            .WithMessage("Task category with the specified ID does not exist.");
    }
}