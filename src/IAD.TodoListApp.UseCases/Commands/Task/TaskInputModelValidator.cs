using FluentValidation;

namespace IAD.TodoListApp.UseCases.Commands.Task;

public class TaskInputModelValidator : AbstractValidator<TaskInputModel>
{
    public TaskInputModelValidator()
    {
        RuleFor(task => task.Title)
            .NotEmpty().WithMessage("Заголовок задачи обязателен.")
            .Length(5, 100).WithMessage("Заголовок задачи должен быть длиной от 5 до 100 символов.");

        RuleFor(task => task.Description)
            .NotEmpty().WithMessage("Описание задачи обязательно.")
            .Length(10, 1000).WithMessage("Описание задачи должно быть длиной от 10 до 1000 символов.");

        RuleFor(task => task.DueDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("Дата завершения задачи должна быть в будущем.");

        RuleFor(task => task.Priority)
            .IsInEnum()
            .WithMessage("Недопустимое значение приоритета задачи.");

        RuleFor(task => task.CategoryId)
            .GreaterThan(0).When(task => task.CategoryId.HasValue)
            .WithMessage("Идентификатор категории задачи должен быть положительным числом.");
    }
}
