using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.User;
using IAD.TodoListApp.UseCases.User.Validators;
using IAD.TodoListApp.UseCases.TaskCategory.Models;
using IAD.TodoListApp.UseCases.TaskCategory.Validators;

namespace IAD.TodoListApp.UseCases.TaskCategory.Commands.UpdateTaskCategoryCommand;

/// <summary>
/// Команда обновления категории задач.
/// </summary>
/// <param name="TaskCategoryId"> Идентификатор категории задач. </param>
/// <param name="UserId"> Идентификатор пользователя. </param>
/// <param name="Model"> Модель категории задач. </param>
public record UpdateTaskCategoryCommand(long TaskCategoryId, long UserId, TaskCategoryInputModel Model) : IValidatableCommand<Unit>;

/// <summary>
/// Валидатор для команды обновления категории задач.
/// </summary>
public class UpdateTaskCategoryCommandValidator : AbstractValidator<UpdateTaskCategoryCommand>
{
    public UpdateTaskCategoryCommandValidator(ITaskCategoryRepository taskCategoryRepository,
                                              IUserRepository userRepository,
                                              IHttpContextAccessor httpContextAccessor)
    {
        RuleFor(x => x.Model)
            .NotNull().WithMessage("Model is required")
            .SetValidator(new TaskCategoryInputModelValidator(taskCategoryRepository, httpContextAccessor));

        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.TaskCategoryId)
            .NotNull().WithMessage("TaskCategoryId is required!")
            .SetValidator(new TaskCategoryExistsValidator(taskCategoryRepository))
            .SetValidator(new TaskCategoryAvailableValidator(taskCategoryRepository, httpContextAccessor));
    }
}
