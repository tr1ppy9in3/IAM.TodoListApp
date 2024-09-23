using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.UseCases.Abstractions;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TodoTask.Queries;
using IAD.TodoListApp.UseCases.TodoTask.Commands.CreateTask;
using IAD.TodoListApp.UseCases.TodoTask.Commands.UpdateTask;
using IAD.TodoListApp.UseCases.TodoTask.Commands.DeleteTask;
using IAD.TodoListApp.UseCases.TodoTask.Models;

namespace IAD.TodoListApp.Service.Controllers;

/// <summary>
/// Контроллер для взаимодействия с задачами.
/// </summary>

[Route("api/tasks")]
[ApiController]
[Authorize(Roles = "RegularUser")]
public class TaskController(IMediator mediator, IUserAccessor userAccessor) : ControllerBase
{
    /// <summary>
    /// Медиатор.
    /// </summary>
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Сервис для доступа к данным авторизованного пользователя.
    /// </summary>
    private readonly IUserAccessor _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));

    /// <summary>
    /// Получить список доступных пользователю задач.
    /// </summary>
    /// <response code="200"> Успешно. Возвращает список задач пользователя. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [ProducesResponseType(type: typeof(IAsyncEnumerable<TaskModel>), 200)]
    [ProducesResponseType(400)]
    [HttpGet]
    public IAsyncEnumerable<TaskModel> GetAvailableTasks()
    {
        long userId = _userAccessor.GetUserId();
        return mediator.CreateStream(new GetAvailableTasksQuery(userId)); ;
    }

    /// <summary>
    /// Получить задачу по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор пользователя. </param>
    /// <returns> Задачу. </returns>
    /// <response code="200"> Успешно. Возвращает найденную задачу. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [ProducesResponseType( type: typeof(TaskModel), 200)]
    [ProducesResponseType(400)]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetTaskById(long id)
    {
        // TODO: Сделать проверку на принадлежность пользователю задачи;
        var result = await _mediator.Send(new GetTaskByIdQuery(id));
        return result.ToActionResult();
    }

    /// <summary>
    /// Создать задачу.
    /// </summary>
    /// <param name="taskInputModel"> Модель задачи. </param>
    /// <response code="201"> Успешно. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create(TaskInputModel taskInputModel)
    {
        long userId = _userAccessor.GetUserId();

        var result =  await _mediator.Send(new CreateTaskCommand(userId, taskInputModel));
        return result.ToActionResult();
    }

    /// <summary>
    /// Обновить задачу.
    /// </summary>
    /// <param name="id"> Идентификатор задачи. </param>
    /// <param name="taskInputModel"> Модель задачи.</param>
    /// <response code="204"> Успешно. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpPut("{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Update(long id, TaskInputModel taskInputModel)
    {
        long userId = _userAccessor.GetUserId();
        // TODO: проверку на создание таски
        var result = await _mediator.Send(new UpdateTaskCommand(taskInputModel));
        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить задачу.
    /// </summary>
    /// <param name="id"> Индетификатор задачи. </param>
    /// <response code="204"> Успешно. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Delete(long id)
    {
        // TODO: Сделать проверку на принадлежность задачи пользователю
        var result = await _mediator.Send(new DeleteTaskCommand(id));
        return result.ToActionResult();
    }
}
