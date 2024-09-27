using MediatR;
using FluentValidation;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TaskCategory.Models;
using IAD.TodoListApp.UseCases.User.Validators;
using IAD.TodoListApp.UseCases.User;
using Microsoft.AspNetCore.Http;

namespace IAD.TodoListApp.UseCases.TaskCategory.Commands.AddTaskCategoryCommand;

/// <summary>
/// Команда добавления категории задач.
/// </summary>
/// <param name="UserId"></param>
/// <param name="Model"></param>
public record class AddTaskCategoryCommand(long UserId, TaskCategoryInputModel Model) : IValidatableCommand<TaskCategoryModel>;

/// <summary>
/// Валидатор для команды добавления категории задач.
/// </summary>
public class AddTaskCategoryCommandValidator : AbstractValidator<AddTaskCategoryCommand>
{
    public AddTaskCategoryCommandValidator(IUserRepository userRepository,
                                           ITaskCategoryRepository taskCategoryRepository,
                                           IHttpContextAccessor httpContextAccessor)
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.Model)
            .NotNull().WithMessage("Model is required!")
            .SetValidator(new TaskCategoryInputModelValidator(taskCategoryRepository, httpContextAccessor));
    }
}
