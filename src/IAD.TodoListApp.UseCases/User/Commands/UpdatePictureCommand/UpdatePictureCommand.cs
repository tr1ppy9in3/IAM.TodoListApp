using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.UseCases.User.Validators;

namespace IAD.TodoListApp.UseCases.User.Commands.UpdatePictureCommand;

/// <summary>
/// Команда смены картинки пользователя.
/// </summary>
/// <param name="Id"> Идентификатор пользователя. </param>
/// <param name="Picture"> Картинка </param>
public record class UpdatePictureCommand(long Id, IFormFile Picture) : IRequest<Result<byte[]>>;

/// <summary>
/// Обработчик команды смены картинки пользователя.
/// </summary>
public class UpdatePictureCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdatePictureCommand, Result<byte[]>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<byte[]>> Handle(UpdatePictureCommand request, CancellationToken cancellationToken)
    {
        if (request.Picture == null || request.Picture.Length == 0)
        {
            return Result<byte[]>.Invalid("Picture is required.");
        }

        byte[] pictureBytes;
        using (var memoryStream = new MemoryStream())
        {
            await request.Picture.CopyToAsync(memoryStream, cancellationToken);
            pictureBytes = memoryStream.ToArray();
        }

        var user = await _userRepository.GetById(request.Id);

        if (user is null)
        {
            return Result<byte[]>.Invalid($"User with Id {request.Id} not found");
        }

        if (user is not RegularUser regularUser)
        {
            return Result<byte[]>.Invalid($"User role doesn't allow to interact with profile picture!");
        }

        regularUser.ProfilePic = pictureBytes;
        await _userRepository.Update(regularUser);

        return Result<byte[]>.SuccessfullyCreated(pictureBytes);
    }
}

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
