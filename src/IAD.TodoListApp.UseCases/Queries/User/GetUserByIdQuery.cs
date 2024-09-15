using MediatR;

using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;

namespace IAD.TodoListApp.UseCases.Queries.User;

public record class GetUserByIdQuery(long Id) : IRequest<Result<UserBase>>;

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