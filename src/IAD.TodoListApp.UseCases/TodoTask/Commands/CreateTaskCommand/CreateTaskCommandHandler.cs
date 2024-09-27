using AutoMapper;
using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using MediatR;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.CreateTaskCommand.CreateTaskCommand;

/// <summary>
/// Обработчик команды для создания задачи.
/// </summary>
public class CreateTaskCommandHandler(ITaskRepository taskRepository,
                                      IMapper mapper) : IRequestHandler<CreateTaskCommand, Result<TaskModel>>
{
    private readonly ITaskRepository _taskRepository = taskRepository
        ?? throw new ArgumentNullException(nameof(taskRepository));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<TaskModel>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<Core.TodoTask>(request.Model);
        task.UserId = request.UserId;

        await _taskRepository.Add(task);
        return Result<TaskModel>.SuccessfullyCreated(_mapper.Map<TaskModel>(task));
    }
}