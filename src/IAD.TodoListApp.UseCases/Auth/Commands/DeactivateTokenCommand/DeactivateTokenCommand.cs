using MediatR;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.Auth.Commands.DeactivateTokenCommand;

/// <summary>
/// Команда деактивации токена.
/// </summary>
/// <param name="Token"> Токен. </param>
public record DeactivateTokenCommand(string Token) : IRequest<Result<Unit>>;
