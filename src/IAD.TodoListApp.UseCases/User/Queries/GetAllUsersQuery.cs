using MediatR;

using IAD.TodoListApp.Core.Abstractions;

namespace IAD.TodoListApp.UseCases.User.Queries;

/// <summary>
/// Запрос на получение всех пользоваталей.
/// </summary>
public record class GetAllUsersQuery : IStreamRequest<UserBase> { }

/// <summary>
/// Обработчик запроса на получение всех пользователей.
/// </summary>
public class GetAllUsersQueryHandler(IUserRepository userRepository) : IStreamRequestHandler<GetAllUsersQuery, UserBase>
{
    private readonly IUserRepository _userRepository = userRepository 
        ?? throw new ArgumentNullException(nameof(userRepository));

    public IAsyncEnumerable<UserBase> Handle(GetAllUsersQuery _, CancellationToken cancellationToken)
    {
        return _userRepository.GetAll();
    }
}