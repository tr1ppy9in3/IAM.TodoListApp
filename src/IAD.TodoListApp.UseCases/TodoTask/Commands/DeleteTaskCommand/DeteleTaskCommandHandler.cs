using MediatR;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands.DeleteTaskCommand.DeleteTaskCommand;

/// <summary>
/// Обработчик команды для удаления задачи.
/// </summary>
public class DeteleTaskCommandHandler(ITaskRepository taskRepository) : IRequestHandler<DeleteTaskCommand, Result<Unit>>
{
    private readonly ITaskRepository _taskRepository = taskRepository
        ?? throw new ArgumentNullException(nameof(taskRepository));

    public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        await _taskRepository.Delete(request.TaskId);
        return Result<Unit>.Empty();
    }
}
