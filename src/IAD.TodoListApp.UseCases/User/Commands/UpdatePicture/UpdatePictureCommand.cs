using MediatR;
using Microsoft.AspNetCore.Http;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Core.Authentication;

namespace IAD.TodoListApp.UseCases.User.Commands.UpdatePicture;

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