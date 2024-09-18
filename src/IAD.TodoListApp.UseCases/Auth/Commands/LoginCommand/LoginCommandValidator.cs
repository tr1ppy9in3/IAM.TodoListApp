using FluentValidation;

namespace IAD.TodoListApp.UseCases.Auth.Commands.LoginCommand;

/// <summary>
/// Валидаотр команды для авторизации.
/// </summary>
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Login)
           .NotEmpty().WithMessage("Login is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
