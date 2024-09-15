using MediatR;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

using IAD.TodoListApp.Core.Options;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;

namespace IAD.TodoListApp.UseCases.Commands.User;

public class ChangePasswordCommand(long userId, string password) : IRequest<Result<Unit>>
{
    /// <summary>
    /// Новый пароль.
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = password;

    /// <summary>
    /// Индетификатор пользователя.
    /// </summary>
    public long UserId { get; set; } = userId;
}

public class ChangePasswordComandHandler(IUserRepository userRepository,
                                    IOptions<PasswordOptions> passwordOptions) : IRequestHandler<ChangePasswordCommand, Result<Unit>>
{
private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
private readonly PasswordOptions _passwordOptions = passwordOptions?.Value ?? throw new ArgumentNullException(nameof(passwordOptions));


public async Task<Result<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
{
    if (!Validator.TryValidateObject(request, new ValidationContext(request), null, true))
    {
        return Result<Unit>.Invalid("Invalid password format!");
    }

    var user = await _userRepository.GetById(request.UserId);
    if (user == null)
    {
        return Result<Unit>.Invalid($"User with {request.UserId} doesn't exist");
    }

    await _userRepository.ChangePassword(user, request.Password, _passwordOptions);

    return Result<Unit>.Empty();
}
}
