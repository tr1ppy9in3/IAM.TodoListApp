using FluentValidation;

namespace IAD.TodoListApp.UseCases.User.Commands.ChangeEmail;

/// <summary>
/// Валидатор для команды смены почты пользователя.
/// </summary>
public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email is required.")
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email should be a valid email address.");
    }
}
