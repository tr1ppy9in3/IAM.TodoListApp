using MediatR;
using Microsoft.Extensions.Options;

using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.Core.Enums;
using IAD.TodoListApp.Core.Options;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.User;

namespace IAD.TodoListApp.UseCases.Auth.Commands.RegistrationCommand;

/// <summary>
/// Обработчик команды для регистрации пользователя.
/// </summary>
public class RegistrationCommandHandler(IUserRepository userRepository, IOptions<PasswordOptions> passwordOptions) : IRequestHandler<RegistrationCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    private readonly PasswordOptions _passwordOptions = passwordOptions?.Value
        ?? throw new ArgumentNullException(nameof(passwordOptions));

    public async Task<Result<Unit>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var existingUserWithEmail = await _userRepository.GetByEmail(request.Email);
        if (existingUserWithEmail is not null)
        {
            return Result<Unit>.Conflict("User with this email already exists!");
        }

        var existingUserWithLogin = await _userRepository.GetByLogin(request.Login);
        if (existingUserWithLogin is not null)
        {
            return Result<Unit>.Conflict("User with this login already exists!");
        }

        if (!Enum.TryParse(request.Role, true, out UserRole parsedRole) || !Enum.IsDefined(typeof(UserRole), parsedRole))
        {
            return Result<Unit>.Invalid("Invalid user role!");
        }

        UserBase user;
        switch (parsedRole)
        {
            case UserRole.Admin:
                user = new Admin { Login = request.Login, Email = request.Email };
                break;
            case UserRole.RegularUser:
                user = new RegularUser { Login = request.Login, Email = request.Email };
                break;
            default:
                return Result<Unit>.Invalid("Invalid user role!");
        }

        user.SetPassword(request.Password, _passwordOptions);
        await _userRepository.Add(user);

        return Result<Unit>.SuccessfullyCreated(Unit.Value);
    }
}