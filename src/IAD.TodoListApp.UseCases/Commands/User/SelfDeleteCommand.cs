using MediatR;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;

namespace IAD.TodoListApp.UseCases.Commands.User;

public sealed record class SelfDeleteCommand(long Id) : IRequest<Result<Unit>>;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<SelfDeleteCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<Unit>> Handle(SelfDeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);
        if (user == null)
        {
            return Result<Unit>.Invalid($"User with {request.Id} doesn't exist");
        }

        await _userRepository.Delete(user);

        return Result<Unit>.Empty();
    }
}

