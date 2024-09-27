using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace IAD.TodoListApp.UseCases.TaskCategory.Models;

/// <summary>
/// Входная модель категории задач.
/// </summary>
public class TaskCategoryInputModel
{
    /// <summary>
    /// Название категории задачи.
    /// Например, "Работа", "Личное", "Проект" и т.д.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание категории задачи.
    /// Дополнительная информация о категории, которая может помочь пользователю понять её назначение.
    /// </summary>
    public required string Description { get; set; }
}

/// <summary>
/// Валидатор для входной модели категории задач.
/// </summary>
public class TaskCategoryInputModelValidator : AbstractValidator<TaskCategoryInputModel>
{
    private readonly ITaskCategoryRepository _taskCategoryRepository;
    private readonly HttpContext _httpContext;

    public TaskCategoryInputModelValidator(ITaskCategoryRepository taskCategoryRepository,
                                           IHttpContextAccessor httpContextAccessor)
    {
        _taskCategoryRepository = taskCategoryRepository
            ?? throw new ArgumentNullException(nameof(taskCategoryRepository));
        _httpContext = httpContextAccessor.HttpContext
            ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название категории задачи не должно быть пустым.")
            .Length(3, 100).WithMessage("Название должно содержать от 3 до 100 символов.")
            .MustAsync(BeUniqueName).WithMessage("Категория с таким названием уже существует.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Описание категории задачи не должно быть пустым.")
            .MaximumLength(500).WithMessage("Описание не может содержать более 500 символов.");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        if (!long.TryParse(_httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out long userId))
        {
            return false;
        }

        var existingCategory = await _taskCategoryRepository.GetByName(name);
        return existingCategory == null || existingCategory.UserId == userId;
    }
}
