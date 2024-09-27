using MediatR;
using AutoMapper;
using FluentValidation;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TaskCategory;
using IAD.TodoListApp.UseCases.User;
using IAD.TodoListApp.UseCases.TodoTask.Models;
using IAD.TodoListApp.UseCases.User.Validators;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.CreateTaskCommand;

/// <summary>
/// Команда для создания задачи.
/// </summary>
/// <param name="UserId"> Идентификатор пользователя. </param>
/// <param name="Model"> Модель задачи. </param>
public record CreateTaskCommand(long UserId, TaskInputModel Model) : IValidatableCommand<TaskModel>;

/// <summary>
/// Валидатор для команды создания задачи.
/// </summary>
public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator(IUserRepository userRepository,
                                      ITaskCategoryRepository taskCategoryRepository)
    {
        RuleFor(x => x.UserId)
           .NotNull().WithMessage("UserId is required!")
           .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Task model is required!")
            .SetValidator(new TaskInputModelValidator(taskCategoryRepository));
    }
}