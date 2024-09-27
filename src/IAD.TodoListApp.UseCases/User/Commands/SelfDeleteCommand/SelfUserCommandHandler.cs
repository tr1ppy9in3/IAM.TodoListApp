using MediatR;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.User.Commands.SelfDeleteCommand;

/// <summary>
/// Обработчик команды удаления пользователя.
/// </summary>
public class SelfUserCommandHandler(IUserRepository userRepository) : IRequestHandler<SelfDeleteCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<Unit>> Handle(SelfDeleteCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(request.Id);
        return Result<Unit>.Empty();
    }
}