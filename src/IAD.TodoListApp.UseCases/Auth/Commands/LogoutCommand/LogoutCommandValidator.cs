using FluentValidation;

namespace IAD.TodoListApp.UseCases.Auth.Commands.LogoutCommand;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required!");
    }
}
