using MediatR;
using FluentValidation;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TaskCategory.Validators;
using Microsoft.AspNetCore.Http;

namespace IAD.TodoListApp.UseCases.TaskCategory.Commands.DeleteTaskCategoryCommand;

/// <summary>
/// Команда удаления категории задачи.
/// </summary>
/// <param name="TaskCategoryId"> Идентификатор категории. </param>
public record class DeleteTaskCategoryCommand(long TaskCategoryId) : IValidatableCommand<Unit>;

/// <summary>
/// Валидатор команды удаления категории задачи.
/// </summary>
public class DeleteTaskCategoryCommandValidator : AbstractValidator<DeleteTaskCategoryCommand>
{
    public DeleteTaskCategoryCommandValidator(ITaskCategoryRepository taskCategoryRepository,
                                              IHttpContextAccessor httpContextAccessor)
    {
        RuleFor(x => x.TaskCategoryId)
            .NotNull().WithMessage("TaskCategoryId is required!")
            .SetValidator(new TaskCategoryExistsValidator(taskCategoryRepository))
            .SetValidator(new TaskCategoryAvailableValidator(taskCategoryRepository, httpContextAccessor));
    }
}