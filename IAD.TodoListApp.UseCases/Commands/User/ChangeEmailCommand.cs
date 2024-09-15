using MediatR;
using System.ComponentModel.DataAnnotations;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;

namespace IAD.TodoListApp.UseCases.Commands.User;

public class ChangeEmailCommand(long userId, string email) : IRequest<Result<Unit>>
{
    /// <summary>
    /// Новый email.
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = email;

    /// <summary>
    /// Индетификатор пользователя, у которого меняется email.
    /// </summary>
    public long UserId { get; set; } = userId;
}

public class ChangeEmailCommandHandler(IUserRepository userRepository) : IRequestHandler<ChangeEmailCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<Unit>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        if (!Validator.TryValidateObject(request, new ValidationContext(request), null, true))
        {
            return Result<Unit>.Invalid("Invalid request");
        }

        var user = await _userRepository.GetById(request.UserId);

        if (user == null)
        {
            return Result<Unit>.Invalid($"User with {request.UserId} doesn't exist");
        }

        await _userRepository.ChangeEmail(user, request.Email);

        return Result<Unit>.Empty();
    }
}
