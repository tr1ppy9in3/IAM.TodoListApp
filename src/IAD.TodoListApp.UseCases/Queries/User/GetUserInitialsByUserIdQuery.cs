using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;
using MediatR;

namespace IAD.TodoListApp.UseCases.Queries.User;

public record class GetUserInitialsByUserIdQuery(long Id) : IRequest<Result<UserInitialsModel>>;

public class GetUserInitialsByUserIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserInitialsByUserIdQuery, Result<UserInitialsModel>>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

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