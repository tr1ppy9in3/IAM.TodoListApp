using FluentValidation;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.UpdateTask;

/// <summary>
/// Валидатор команды для обновления задачи.
/// </summary>
public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Task model is required!")
            .SetValidator(new TaskInputModelValidator());
    }
}
