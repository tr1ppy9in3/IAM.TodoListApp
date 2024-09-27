using MediatR;

using IAD.TodoListApp.Packages;
using DeactivateTokenCommandType = IAD.TodoListApp.UseCases.Auth.Commands.DeactivateTokenCommand.DeactivateTokenCommand;

namespace IAD.TodoListApp.UseCases.Auth.Commands.LogoutCommand;

/// <summary>
/// Обработчик команды деавторизации.
/// </summary>
public class LogoutCommandHandler(IMediator mediator) : IRequestHandler<LogoutCommand, Result<Unit>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<Result<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeactivateTokenCommandType(request.Token), cancellationToken);
        return result;
    }
}
