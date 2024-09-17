using MediatR;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;

namespace IAD.TodoListApp.UseCases.Commands.Task;

public record DeleteTaskCommand(long TaskId) : IRequest<Result<Unit>>;

public class DeteleTaskCommandHandler(ITaskRepository taskRepository) : IRequestHandler<DeleteTaskCommand, Result<Unit>>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));

    public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetById(request.TaskId);

        if (task is null)
        {
            return Result<Unit>.Invalid($"Unable to find task with Id {request.TaskId}");
        }

        await _taskRepository.Delete(task);
        return Result<Unit>.Empty();
    }
}