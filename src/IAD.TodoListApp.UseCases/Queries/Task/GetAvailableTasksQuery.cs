using AutoMapper;
using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;
using MediatR;

namespace IAD.TodoListApp.UseCases.Queries.Task;

public record class GetAvailableTasksQuery(long UserId) : IStreamRequest<TaskModel>;

public class GetAvailableTasksQueryHandler(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper) : IStreamRequestHandler<GetAvailableTasksQuery, TaskModel>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async IAsyncEnumerable<TaskModel> Handle(GetAvailableTasksQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        if (user is not null)
        {
            await foreach (var task in _taskRepository.GetAllAvailable(request.UserId))
            {
                yield return _mapper.Map<TaskModel>(task);
            }
        }

    }
}