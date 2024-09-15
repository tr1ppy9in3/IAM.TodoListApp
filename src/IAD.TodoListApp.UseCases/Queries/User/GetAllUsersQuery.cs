using MediatR;

using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;

namespace IAD.TodoListApp.UseCases.Queries.User;
public record class GetAllUsersQuery : IStreamRequest<UserBase> { }

public class GetAllUsersQueryHandler(IUserRepository userRepository) : IStreamRequestHandler<GetAllUsersQuery, UserBase>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public IAsyncEnumerable<UserBase> Handle(GetAllUsersQuery _, CancellationToken cancellationToken)
    {
        return _userRepository.GetAll();
    }
}