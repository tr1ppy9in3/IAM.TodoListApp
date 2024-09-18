using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace IAD.TodoListApp.UseCases.User.Commands.UpdatePicture;

/// <summary>
/// Валидатор команды смены картинки пользователя.
/// </summary>
public class UpdatePictureCommandValidator : AbstractValidator<UpdatePictureCommand>
{
    public UpdatePictureCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository))
            .SetValidator(new IsRegularUserValidator(userRepository));

        RuleFor(x => x.Picture)
            .NotNull().WithMessage("Picture is required!")
            .Must(picture => picture.Length > 0).WithMessage("Picture cannot be empty!")
            .Must(picture => picture.Length <= 24 * 1024 * 1024).WithMessage("Picture size must be less than 24MB!")
            .Must(picture => IsValidImageFormat(picture)).WithMessage("Only JPEG or PNG formats are allowed!");
    }

    private bool IsValidImageFormat(IFormFile file)
    {
        var allowedFormats = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        return allowedFormats.Contains(fileExtension);
    }
}
