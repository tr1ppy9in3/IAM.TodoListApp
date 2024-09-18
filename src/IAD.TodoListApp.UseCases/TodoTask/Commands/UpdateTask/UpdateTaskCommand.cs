using AutoMapper;
using MediatR;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.UpdateTask;

/// <summary>
/// Команда для обновления задачи.
/// </summary>
/// <param name="Model"> Модель задачи.</param>
public record UpdateTaskCommand(TaskInputModel Model) : IRequest<Result<Unit>>;

/// <summary>
/// Обработчик команды для обновления задачи.
/// </summary>
public class UpdateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper) : IRequestHandler<UpdateTaskCommand, Result<Unit>>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken _)
    {
        var task = _mapper.Map<Core.TodoTask>(request.Model);
        await _taskRepository.Update(task);

        return Result<Unit>.Empty();
    }
}
