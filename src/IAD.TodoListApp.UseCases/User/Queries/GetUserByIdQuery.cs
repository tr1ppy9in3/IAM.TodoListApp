using MediatR;

using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.User.Queries;

/// <summary>
/// Запрос на получение пользователя по идентификатору.
/// </summary>
/// <param name="Id"> Идентификатор пользователя. </param>
public record class GetUserByIdQuery(long Id) : IRequest<Result<UserBase>>;

/// <summary>
/// Обработчик запроса на получение пользователя по идентификатору.
/// </summary>
public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, Result<UserBase>>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<UserBase>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user is null)
        {
            return Result<UserBase>.Invalid($"User with Id {request.Id} doesn't exist!");
        }

        return Result<UserBase>.Success(user);
    }
}