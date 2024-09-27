using AutoMapper;
using MediatR;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TaskCategory;

namespace IAD.TodoListApp.UseCases.TaskCategory.Queries;

/// <summary>
/// Запрос на получение категории задач по идентификатору.
/// </summary>
/// <param name="TaskCategoryId"> Идентификатор категории задач. </param>
public record class GetTaskCategoryByIdQuery(long TaskCategoryId) : IRequest<Result<TaskCategoryModel>>;

/// <summary>
/// Обработчик запроса на получение задачи по идентификатору.
/// </summary>
public class GetTaskCategoryByIdQueryHandler(ITaskCategoryRepository taskRepository,
                                             IMapper mapper)
    : IRequestHandler<GetTaskCategoryByIdQuery, Result<TaskCategoryModel>>
{
    private readonly ITaskCategoryRepository _taskCategoryRepository = taskRepository
        ?? throw new ArgumentNullException(nameof(taskRepository));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<TaskCategoryModel>> Handle(GetTaskCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var taskCategory = await _taskCategoryRepository.GetById(request.TaskCategoryId);

        if (taskCategory is null)
        {
            return Result<TaskCategoryModel>.Invalid($"Unable to find task with id. {request.TaskCategoryId}");
        }

        return Result<TaskCategoryModel>.Success(_mapper.Map<TaskCategoryModel>(taskCategory));

    }
}
