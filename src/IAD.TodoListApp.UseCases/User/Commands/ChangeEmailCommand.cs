using MediatR;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.User.Commands;

/// <summary>
/// Команда смены почты пользователя.
/// </summary>
/// <param name="userId"> Идентификатор пользователя. </param>
/// <param name="email"> Новая почта пользователя. </param>
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

/// <summary>
/// Обработчик команды смены почты пользователя.
/// </summary>
public class ChangeEmailCommandHandler(IUserRepository userRepository) : IRequestHandler<ChangeEmailCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

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

/// <summary>
/// Валидатор для команды смены почты пользователя.
/// </summary>
public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email is required.")
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email should be a valid email address.");
    }
}
