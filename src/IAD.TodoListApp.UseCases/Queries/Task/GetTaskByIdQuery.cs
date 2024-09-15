using AutoMapper;
using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;
using MediatR;

namespace IAD.TodoListApp.UseCases.Queries.Task;

public record class GetTaskByIdQuery(long TaskId) : IRequest<Result<TaskModel>> ;

public class GetTaskByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper) : IRequestHandler<GetTaskByIdQuery, Result<TaskModel>>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<TaskModel>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetById(request.TaskId);

        if (task is null)
        {
            return Result<TaskModel>.Invalid($"Unable to find task with id. {request.TaskId}");
        }

        return Result<TaskModel>.Success(_mapper.Map<TaskModel>(task));

    }
}
