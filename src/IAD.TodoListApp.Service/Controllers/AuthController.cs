using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAD.TodoListApp.UseCases.Commands.Auth.RegistrationCommand;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Commands.Auth.LoginCommand;
using IAD.TodoListApp.Core;

namespace IAD.TodoListApp.Service.Controllers;

/// <summary>
/// Контроллер для аунтефикации.
/// </summary>
[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Посредник.
    /// </summary>
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

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
    [ProducesResponseType(typeof(Token), 200)]
    [ProducesResponseType(400)]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }
}
