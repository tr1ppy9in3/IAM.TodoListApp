using MediatR;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.User.Queries;

/// <summary>
/// Запрос на получение инициалов пользователя.
/// </summary>
/// <param name="Id"> Идентификатор пользователя. </param>
public record class GetUserInitialsByUserIdQuery(long Id) : IRequest<Result<UserInitialsModel>>;

/// <summary>
/// Обработчик запроса на получение инициалов пользователя.
/// </summary>
public class GetUserInitialsByUserIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserInitialsByUserIdQuery, Result<UserInitialsModel>>
{
    private readonly IUserRepository _userRepository = userRepository 
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<UserInitialsModel>> Handle(GetUserInitialsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user is null)
        {
            return Result<UserInitialsModel>.Invalid($"User with Id {request.Id} doesn't exist!");
        }

        if (user is not RegularUser regularUser)
        {
            return Result<UserInitialsModel>.Invalid($"User role doesn't allow to interact with profile picture!");
        }

        return Result<UserInitialsModel>.Success(new UserInitialsModel
        {
            Name = regularUser.Name,
            Surname = regularUser.Surname
        });
    }
}