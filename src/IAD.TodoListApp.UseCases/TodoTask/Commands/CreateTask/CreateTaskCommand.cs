using MediatR;
using AutoMapper;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.TodoTask.Commands;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.CreateTask;

/// <summary>
/// Команда для создания задачи.
/// </summary>
/// <param name="UserId"> Идентификатор пользователя. </param>
/// <param name="Model"> Модель задачи. </param>
public record CreateTaskCommand(long UserId, TaskInputModel Model) : IRequest<Result<TaskModel>>;

/// <summary>
/// Обработчик команды для создания задачи.
/// </summary>
public class CreateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper) : IRequestHandler<CreateTaskCommand, Result<TaskModel>>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<TaskModel>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<Core.TodoTask>(request.Model);
        task.UserId = request.UserId;

        await _taskRepository.Add(task);
        return Result<TaskModel>.SuccessfullyCreated(_mapper.Map<TaskModel>(task));
    }
}
