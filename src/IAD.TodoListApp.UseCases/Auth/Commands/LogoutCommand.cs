using MediatR;
using FluentValidation;

using IAD.TodoListApp.Packages;
using DeactivateTokenCommandType = IAD.TodoListApp.UseCases.Auth.Commands.DeactivateTokenCommand;

namespace IAD.TodoListApp.UseCases.Auth.Commands;

/// <summary>
/// Команда деавторизации.
/// </summary>
/// <param name="Token"></param>
public record class LogoutCommand(string Token) : IRequest<Result<Unit>>;


/// <summary>
/// Обработчик команды деавторизации.
/// </summary>
public class LogoutCommandHandler(IMediator mediator) : IRequestHandler<LogoutCommand, Result<Unit>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<Result<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeactivateTokenCommandType(request.Token));
        return result;
    }
}

/// <summary>
/// Валидатор команды деавторизации.
/// </summary>
public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required!");
    }
}

