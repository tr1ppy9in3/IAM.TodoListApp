using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.UseCases.Abstractions;
using IAD.TodoListApp.UseCases.Commands.Task;
using IAD.TodoListApp.UseCases.Queries.Task;
using IAD.TodoListApp.Packages;

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

    [ProducesResponseType(type: typeof(IAsyncEnumerable<TaskModel>), 200)]
    [ProducesResponseType(400)]
    [HttpGet]
    public IAsyncEnumerable<TaskModel> GetAvailableTasks()
    {
        long userId = _userAccessor.GetUserId();
        return mediator.CreateStream(new GetAvailableTasksQuery(userId)); ;
    }
        
    [ProducesResponseType( type: typeof(TaskModel), 200)]
    [ProducesResponseType(400)]
    [HttpGet("/{id:long}")]
    public async Task<IActionResult> GetTaskById(long id)
    {
        var result = _mediator.Send(new GetTaskByIdQuery(id));
        throw new NotImplementedException();

        //return result.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create(TaskInputModel taskInputModel)
    {
        long userId = _userAccessor.GetUserId();

        throw new NotImplementedException();
    }

    [HttpPut("/{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Update(long id, TaskInputModel taskInputModel)
    {
        throw new NotImplementedException();

    }

    [HttpDelete("/{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Delete(long id)
    {
        throw new NotImplementedException();
    }
}
