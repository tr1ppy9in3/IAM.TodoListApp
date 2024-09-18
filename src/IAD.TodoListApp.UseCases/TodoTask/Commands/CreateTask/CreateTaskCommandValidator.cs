using FluentValidation;
using IAD.TodoListApp.UseCases.User;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.CreateTask;

/// <summary>
/// Валидатор для команды создания задачи.
/// </summary>
public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserId)
           .NotNull().WithMessage("UserId is required!")
           .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Task model is required!")
            .SetValidator(new TaskInputModelValidator());
    }
}
