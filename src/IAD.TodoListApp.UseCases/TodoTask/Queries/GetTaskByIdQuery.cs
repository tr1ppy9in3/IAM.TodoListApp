using AutoMapper;
using MediatR;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.TodoTask.Queries;

/// <summary>
/// Запрос на получение задачи по идентификатору.
/// </summary>
/// <param name="TaskId"> Идентификатор задачи. </param>
public record class GetTaskByIdQuery(long TaskId) : IRequest<Result<TaskModel>>;

/// <summary>
/// Обработчик запроса на получение задачи по идентификатору.
/// </summary>
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
