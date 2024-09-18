using FluentValidation;

namespace IAD.TodoListApp.UseCases.User.Commands.SelfDelete;

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
