using FluentValidation;
using IAD.TodoListApp.UseCases.TaskCategory;
using IAD.TodoListApp.UseCases.User;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.CreateTask;

/// <summary>
/// Валидатор для команды создания задачи.
/// </summary>
public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator(IUserRepository userRepository, ITaskCategoryRepository taskCategoryRepository)
    {
        RuleFor(x => x.UserId)
           .NotNull().WithMessage("UserId is required!")
           .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Task model is required!")
            .SetValidator(new TaskInputModelValidator());

        RuleFor(x => x.Model.CategoryId)
          .SetValidator(new IsCategoryAvailableForUserValidator(taskCategoryRepository)) 
          .WithMessage("Category is not available for the user.")
          .WithState(x => x.UserId);
    }
}
