using FluentValidation;

using IAD.TodoListApp.Core.Authentication;

namespace IAD.TodoListApp.UseCases.User;

/// <summary>
/// Валидатор для проверки имеет ли пользователь роль "RegularUser"/
/// </summary>
public class IsRegularUserValidator : AbstractValidator<long>
{
    private readonly IUserRepository _userRepository;

    public IsRegularUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(userId => userId)
            .MustAsync(UserExistsAndIsRegularUser)
            .WithMessage("User is not a RegularUser and don't have permission to interact.");
    }

    private async Task<bool> UserExistsAndIsRegularUser(long userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(userId);
        return user is RegularUser;
    }
}