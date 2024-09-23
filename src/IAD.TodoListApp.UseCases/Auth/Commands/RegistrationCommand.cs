using MediatR;
using FluentValidation;

using Microsoft.Extensions.Options;

using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.Core.Enums;
using IAD.TodoListApp.Core.Options;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.User;

namespace IAD.TodoListApp.UseCases.Auth.Commands;

/// <summary>
/// Команда для регистрации пользователя.
/// </summary>
/// <param name="Login"> Логин пользователя. </param>
/// <param name="Password"> Пароль пользователя. </param>
/// <param name="Email"> Email пользователя. </param>
/// <param name="Role"> Роль пользователя.</param>
public sealed record class RegistrationCommand(string Login, string Password, string Email, string Role) : IRequest<Result<Unit>>;

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

/// <summary>
/// Валидатор команды для регистрации пользователя.
/// </summary>
public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .MaximumLength(50).WithMessage("Login must be less than 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email is required.")
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email should be a valid email address.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Must(BeAValidRole).WithMessage("Invalid role specified.");
    }

    private bool BeAValidRole(string role)
    {
        return Enum.TryParse<UserRole>(role, true, out _) && Enum.IsDefined(typeof(UserRole), role);
    }
}