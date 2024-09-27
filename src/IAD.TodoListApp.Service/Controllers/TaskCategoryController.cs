using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Service.Infrastructure;
using IAD.TodoListApp.UseCases.TaskCategory.Queries;
using IAD.TodoListApp.UseCases.TaskCategory.Models;
using IAD.TodoListApp.UseCases.TaskCategory.Commands.AddTaskCategoryCommand;
using IAD.TodoListApp.UseCases.TaskCategory.Commands.UpdateTaskCategoryCommand;
using IAD.TodoListApp.UseCases.TaskCategory.Commands.DeleteTaskCategoryCommand;

namespace IAD.TodoListApp.Service.Controllers;

/// <summary>
/// Контроллер для взаимодействия с категориями задач.
/// </summary>
[Route("api/task_categories")]
[ApiController]
[Authorize(Roles = "RegularUser")]
public class TaskCategoryController(IMediator mediator, UserAccessor userAccessor) : ControllerBase
{
    /// <summary>
    /// Посредник.
    /// </summary>
    private readonly IMediator _mediator = mediator 
        ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Сервис для доступа к данным авторизованного пользователя.
    /// </summary>
    private readonly UserAccessor _userAccessor = userAccessor 
        ?? throw new ArgumentNullException(nameof(userAccessor));

    /// <summary>
    /// Получить список доступных пользователю категорий задач.
    /// </summary>
    /// <response code="200"> Успешно. Возвращает список категорий задач пользователя. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [ProducesResponseType(type: typeof(IAsyncEnumerable<TaskCategoryModel>), 200)]
    [ProducesResponseType(400)]
    [HttpGet]
    public IAsyncEnumerable<TaskCategoryModel> GetAvailableTaskCategories()
    {
        var userId = _userAccessor.GetUserId();
        return _mediator.CreateStream(new GetAvailableTaskCategoriesQuery(userId));
        
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
    public async Task<IActionResult> GetTaskCategoryById(long id)
    {
        var result = await _mediator.Send(new GetTaskCategoryByIdQuery(id));
        return result.ToActionResult();
    }

    /// <summary>
    /// Создать категорию задач.
    /// </summary>
    /// <param name="taskCategoryInputModel"> Модель категории задачи. </param>
    /// <response code="201"> Успешно. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create(TaskCategoryInputModel taskCategoryInputModel)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new AddTaskCategoryCommand(userId, taskCategoryInputModel));
        return result.ToActionResult();
    }

    /// <summary>
    /// Обновить категорию задач.
    /// </summary>
    /// <param name="id"> Идентификатор категории задач. </param>
    /// <param name="taskCategoryInputModel"> Модель категории задач.</param>
    /// <response code="204"> Успешно. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpPut("{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Update(long id, TaskCategoryInputModel taskCategoryInputModel)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UpdateTaskCategoryCommand(id, userId, taskCategoryInputModel));
        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить категорию задач.
    /// </summary>
    /// <param name="id"> Индетификатор категории задач. </param>
    /// <response code="204"> Успешно. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteTaskCategoryCommand(id));
        return result.ToActionResult();
    }
}
