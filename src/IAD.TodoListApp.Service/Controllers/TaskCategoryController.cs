using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.UseCases.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAD.TodoListApp.Service.Controllers;

/// <summary>
/// Контроллер для взаимодействия с категориями задач.
/// </summary>
[Route("api/task_categories")]
[ApiController]
[Authorize(Roles = "RegularUser")]
public class TaskCategoryController(IMediator mediator, IUserAccessor userAccessor) : ControllerBase
{
    /// <summary>
    /// Посредник.
    /// </summary>
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


    /// <summary>
    /// Сервис для доступа к данным авторизованного пользователя.
    /// </summary>
    private readonly IUserAccessor _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));

    /// <summary>
    /// Получить список доступных пользователю категорий задач.
    /// </summary>
    /// <response code="200"> Успешно. Возвращает список категорий задач пользователя. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [ProducesResponseType(type: typeof(IAsyncEnumerable<TaskCategoryModel>), 200)]
    [ProducesResponseType(400)]
    [HttpGet]
    public Task<IActionResult> GetAvailableTaskCategories()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор категории. </param>
    /// <response code="200"> Успешно. Возвращает найденную категорию. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [ProducesResponseType(type: typeof(TaskCategoryModel), 200)]
    [ProducesResponseType(400)]
    [HttpGet("{id:long}")]
    public Task<IActionResult> GetTaskCategoryById(long id)
    {
        throw new NotImplementedException();
    }


}
