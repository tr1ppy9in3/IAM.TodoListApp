using MediatR;
using FluentValidation;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.User.Validators;

namespace IAD.TodoListApp.UseCases.User.Commands.SelfDeleteCommand;

/// <summary>
/// Команда удаления пользователя.
/// </summary>
/// <param name="Id"> Идентификатор пользователя. </param>
public sealed record class SelfDeleteCommand(long Id) : IRequest<Result<Unit>>;


/// <summary>
/// Валидатор команды удаления пользователя.
/// </summary>
public class SelfDeleteCommandValidator : AbstractValidator<SelfDeleteCommand>
{
    public SelfDeleteCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required!")
            .SetValidator(new UserExistsValidator(userRepository));
    }
}
