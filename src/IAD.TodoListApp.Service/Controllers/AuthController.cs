using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.Auth.Commands.LoginCommand;
using IAD.TodoListApp.UseCases.Auth.Commands.RegistrationCommand;
using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.UseCases.Auth.Commands.LogoutCommand;
using IAD.TodoListApp.Core.Authentication;
using System.Runtime.InteropServices;
using IAD.TodoListApp.Service.Infrastructure;

namespace IAD.TodoListApp.Service.Controllers;

/// <summary>
/// Контроллер для аунтефикации.
/// </summary>
[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController(IMediator mediator, UserAccessor userAccessor) : ControllerBase
{
    /// <summary>
    /// Посредник.
    /// </summary>
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Сервис для доступа к данным авторизованного пользователя.
    /// </summary>
    private readonly UserAccessor _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));

    /// <summary>
    /// Регистрация.
    /// </summary>
    /// <response code="200"> Успешно. </response>
    /// <response code="400"> Некоретный запрос. </response>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [HttpPost("registration")]
    public async Task<IActionResult> Registration(RegistrationCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }
    /// <summary>
    /// Авторизация.
    /// </summary>
    /// <response code="200"> Успешно. </response>
    /// <returns> Токен. </returns>
    /// <response code="400"> Некоретный запрос. </response>
    [ProducesResponseType(typeof(TokenModel), 200)]
    [ProducesResponseType(400)]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }

    /// <summary>
    /// Выход.
    /// </summary>
    /// <response code="204"> Успешно. </response>
    /// <returns> Токен. </returns>
    /// <response code="400"> Некоретный запрос. </response>
    [Authorize(Roles = "RegularUser")]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var token = _userAccessor.GetToken();
        
        var result = await _mediator.Send(new LogoutCommand(token!));
        return result.ToActionResult();
    }
}
