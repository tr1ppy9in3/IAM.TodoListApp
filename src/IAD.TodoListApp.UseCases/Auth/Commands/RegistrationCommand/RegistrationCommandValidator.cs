using FluentValidation;
using IAD.TodoListApp.Core.Enums;

namespace IAD.TodoListApp.UseCases.Auth.Commands.RegistrationCommand;

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .MaximumLength(50).WithMessage("Login must be less than 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email is required.")
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email should be a valid email address.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Must(BeAValidRole).WithMessage("Invalid role specified.");
    }

    private bool BeAValidRole(string role)
    {
        return Enum.TryParse<UserRole>(role, true, out _) && Enum.IsDefined(typeof(UserRole), role);
    }
}