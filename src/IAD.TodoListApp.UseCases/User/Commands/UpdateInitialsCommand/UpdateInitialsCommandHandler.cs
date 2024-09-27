using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.Packages;

using MediatR;

namespace IAD.TodoListApp.UseCases.User.Commands.UpdateInitialsCommand;

/// <summary>
/// Обработчик команды обновления инициалов пользователя.
/// </summary>
public class UpdateInitialsCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateInitialsCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<Unit>> Handle(UpdateInitialsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user is null)
        {
            return Result<Unit>.Invalid($"User with Id {request.Id} not found");
        }

        if (user is not RegularUser regularUser)
        {
            return Result<Unit>.Invalid($"User role doesn't allow to interact with profile picture!");
        }

        regularUser.Name = request.Model.Name;
        regularUser.Surname = request.Model.Surname;

        await _userRepository.Update(regularUser);

        return Result<Unit>.Empty();
    }
}
