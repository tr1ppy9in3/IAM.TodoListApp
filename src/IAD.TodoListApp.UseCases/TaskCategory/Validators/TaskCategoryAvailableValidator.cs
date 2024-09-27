using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace IAD.TodoListApp.UseCases.TaskCategory.Validators;

/// <summary>
/// Валидатор для проверки, доступна ли категория задач для текущего пользователя.
/// </summary>
public class TaskCategoryAvailableValidator : AbstractValidator<long>
{
    private readonly ITaskCategoryRepository _taskCategoryRepository;
    private readonly HttpContext _httpContext;

    public TaskCategoryAvailableValidator(ITaskCategoryRepository taskCategoryRepository,
                                          IHttpContextAccessor httpContextAccessor)
    {
        _taskCategoryRepository = taskCategoryRepository
            ?? throw new ArgumentNullException(nameof(taskCategoryRepository));
        _httpContext = httpContextAccessor.HttpContext
            ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        RuleFor(taskCategoryId => taskCategoryId)
            .MustAsync(BeAvailableForUser)
            .WithMessage("Task category isn't available for the user!");
    }

    private async Task<bool> BeAvailableForUser(long categoryId, CancellationToken cancellationToken)
    {
        if (!long.TryParse(_httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out long userId))
        {
            return false;
        }

        return await _taskCategoryRepository.IsTaskCategoryAvailable(categoryId, userId);
    }
}
