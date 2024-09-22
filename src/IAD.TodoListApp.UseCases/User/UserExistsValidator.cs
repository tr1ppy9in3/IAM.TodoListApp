using FluentValidation;

namespace IAD.TodoListApp.UseCases.User;

/// <summary>
/// Валидатор для проверки на существование пользователя.
/// </summary>
public class UserExistsValidator : AbstractValidator<long>
{
    public UserExistsValidator(IUserRepository userRepository)
    {

        RuleFor(userId => userId)
            .MustAsync(async (userId, cancellation) =>
            {
                var user = await userRepository.GetById(userId);
                return user != null;
            })
            .WithMessage("User with Id {PropertyValue} doesn't exist.");
    }
}
