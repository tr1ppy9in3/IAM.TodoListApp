using MediatR;
using FluentValidation;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.UseCases.User.Models;
using IAD.TodoListApp.UseCases.User.Validators;

namespace IAD.TodoListApp.UseCases.User.Commands.UpdateInitialsCommand;

/// <summary>
/// Команда обновления инициалов пользователя.
/// </summary>
/// <param name="Id"> Идентификатор пользователя </param>
/// <param name="Model"> Модель инициалов. </param>
public record class UpdateInitialsCommand(long Id, UserInitialsModel Model) : IRequest<Result<Unit>>;

/// <summary>
/// Валидатор команды обновления инициалов пользователя.
/// </summary>
public class UpdateInitialsCommandValidator : AbstractValidator<UpdateInitialsCommand>
{
    public UpdateInitialsCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository))
            .SetValidator(new IsRegularUserValidator(userRepository));

        RuleFor(x => x.Model)
            .NotNull().WithMessage("Model is required!")
            .SetValidator(new UserInitialsModelValidator());
    }
}