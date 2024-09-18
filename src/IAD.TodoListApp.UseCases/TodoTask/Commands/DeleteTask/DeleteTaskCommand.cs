using MediatR;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.DeleteTask;

/// <summary>
/// Команда для удаления задачи.
/// </summary>
/// <param name="TaskId"> Идентификатор задачи. </param>
public record DeleteTaskCommand(long TaskId) : IRequest<Result<Unit>>;

/// <summary>
/// Обработчик команды для удаления задачи.
/// </summary>
public class DeteleTaskCommandHandler(ITaskRepository taskRepository) : IRequestHandler<DeleteTaskCommand, Result<Unit>>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));

    public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        await _taskRepository.Delete(request.TaskId);
        return Result<Unit>.Empty();
    }
}