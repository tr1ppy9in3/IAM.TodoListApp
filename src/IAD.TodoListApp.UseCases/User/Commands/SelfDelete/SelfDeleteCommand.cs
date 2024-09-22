using MediatR;

using IAD.TodoListApp.Packages;
using System.Runtime.CompilerServices;

namespace IAD.TodoListApp.UseCases.User.Commands.SelfDelete;

/// <summary>
/// Команда удаления пользователя.
/// </summary>
/// <param name="Id"> Идентификатор пользователя. </param>
public sealed record class SelfDeleteCommand(long Id) : IRequest<Result<Unit>>;

/// <summary>
/// Обработчик команды удаления пользователя.
/// </summary>
public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<SelfDeleteCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository 
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<Unit>> Handle(SelfDeleteCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(request.Id);
        return Result<Unit>.Empty();
    }
}

