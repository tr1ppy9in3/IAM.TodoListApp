using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.UseCases.User.Queries;
using IAD.TodoListApp.UseCases.User.Commands.UpdateInitials;
using IAD.TodoListApp.UseCases.User.Commands.UpdatePicture;
using IAD.TodoListApp.UseCases.User.Commands.ChangeEmail;
using IAD.TodoListApp.UseCases.User.Commands.ChangePassword;
using IAD.TodoListApp.UseCases.User.Commands.SelfDelete;
using IAD.TodoListApp.Service.Infrastructure;

namespace IAD.TodoListApp.Service.Controllers;

/// <summary>
/// Контроллер для взаимодействия с текующим пользователем
/// </summary>
[Route("api/user")]
[ApiController]
public class UserController(IMediator mediator, UserAccessor userAccessor) : ControllerBase
{
    /// <summary>
    /// Посредник
    /// </summary>
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Сервис для доступа к данным авторизованного пользователя.
    /// </summary>
    private readonly UserAccessor _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));

    /// <summary>
    /// Получить инициалы текущего пользователя
    /// </summary>
    /// <returns></returns>
    /// <response code="200"> Успешно. Возвращает найденный профиль. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpGet("initials")]
    [ProducesResponseType(typeof(UserInitialsModel), 200)]
    [ProducesResponseType(400)]
    [Authorize(Roles = "RegularUser")]
    public async Task<IActionResult> GetInitials()
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new GetUserInitialsByUserIdQuery(userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Изменить инициалы текущего пользователя
    /// </summary>
    /// <response code="204"> Успешно без контента. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpPut("initials")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [Authorize(Roles = "RegularUser")]
    public async Task<IActionResult> UpdateInitials(UserInitialsModel request)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UpdateInitialsCommand(userId, request));
        return result.ToActionResult();
    }

    /// <summary>
    /// Получить картинку профиля текущего пользователя
    /// </summary>
    /// <response code="200"> Успешно. Возвращает картинку профиля. </response>
    /// <response code="404"> Пользователь или картинка не найдены. </response>
    [HttpGet("picture")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Authorize(Roles = "RegularUser")]
    public async Task<IActionResult> GetPicture()
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new GetUserPictureByIdQuery(userId));

        if (result.IsSuccess)
        {
            return File(result.GetValue(), "image/jpeg");
        }

        return result.ToActionResult();
    }

    /// <summary>
    /// Изменить картинку профиля текущего пользователя
    /// </summary>
    /// <param name="picture"> Новая картинка профиля</param>
    /// <response code="201"> Успешно. Возвращает картинку. </response>
    /// <response code="400"> Некорректный запрос. </response>
    /// <response code="415"> Неподдерживаемый тип медиа. </response>
    [HttpPost("picture")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(415)]
    [Authorize(Roles = "RegularUser")]

    public async Task<IActionResult> UpdatePicture(IFormFile picture)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UpdatePictureCommand(userId, picture));

        if (result.IsSuccess)
        {
            return File(result.GetValue(), "image/jpeg");
        }

        return result.ToActionResult();
    }


    /// <summary>
    /// Изменить почту текущего пользователя
    /// </summary>
    /// <response code="204"> Успешно без контента. </response>
    /// <response code="400"> Некорректный запрос. </response>
    /// <param name="email"> Измененная почта</param>
    [HttpPost("/change-email")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [Authorize(Roles = "Admin, RegularUser")]
    public async Task<IActionResult> ChangeEmail(string email)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new ChangeEmailCommand(userId, email));
        return result.ToActionResult();
    }

    /// <summary>
    /// Изменить пароль текущего пользователя
    /// </summary>
    /// <response code="204"> Успешно без контента.</response>
    /// <response code="400"> Некорректный запрос. </response>
    /// <param name="password"> Измененный пароль</param>
    [HttpPost("/change-password")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [Authorize(Roles = "Admin, RegularUser")]
    public async Task<IActionResult> ChangePassword(string password)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new ChangePasswordCommand(userId, password));
        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить текущего пользователя
    /// </summary>
    /// <response code="204"> Успешно без контента. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [Authorize(Roles = "Admin, RegularUser")]
    [HttpDelete("")]
    public async Task<IActionResult> DeleteUser()
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new SelfDeleteCommand(userId));
        return result.ToActionResult();
    }
}
