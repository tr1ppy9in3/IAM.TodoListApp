using FluentValidation;

namespace IAD.TodoListApp.UseCases.User.Commands.ChangePassword;

/// <summary>
/// Валидатор команды смены пароля пользователя.
/// </summary>
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.");

    }
}
