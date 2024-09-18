using FluentValidation;

namespace IAD.TodoListApp.UseCases.User.Commands.UpdateInitials;

/// <summary>
/// Валидатор команды обновления инициалов пользователя.
/// </summary>
public class UpdateInitialsCommandValidator : AbstractValidator<UpdateInitialsCommand>
{
    public UpdateInitialsCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository)) 
            .SetValidator(new IsRegularUserValidator(userRepository));

        RuleFor(x => x.Model)
            .NotNull().WithMessage("Model is required!")
            .SetValidator(new UserInitialsModelValidator());
    }
}
