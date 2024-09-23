using FluentValidation;
using IAD.TodoListApp.Core.Enums;

namespace IAD.TodoListApp.UseCases.TodoTask.Models;

/// <summary>
/// Входная модель задачи.
/// </summary>
public class TaskInputModel
{
    /// <summary>
    /// Заголовок задачи.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Описание задачи.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Дата и время, к которой задача должна быть завершена.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Приоритет задачи.
    /// </summary>
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    /// <summary>
    /// Идентификатор категории задачи.
    /// </summary>
    public long? CategoryId { get; set; }
}

/// <summary>
/// Валидатор входной модели задачи.
/// </summary>
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