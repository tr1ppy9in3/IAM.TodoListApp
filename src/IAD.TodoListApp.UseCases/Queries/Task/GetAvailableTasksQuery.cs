using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;
using MediatR;

namespace IAD.TodoListApp.UseCases.Queries.Task;

public record class GetAvailableTasksQuery(long UserId) : IStreamRequest<TodoTask>;

public class GetAvailableTasksQueryHandler(ITaskRepository taskRepository) : IStreamRequestHandler<GetAvailableTasksQuery, TodoTask>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));

    public IAsyncEnumerable<TodoTask> Handle(GetAvailableTasksQuery request, CancellationToken cancellationToken)
    {
        return _taskRepository.GetAllAvailable(request.UserId);
    }
}