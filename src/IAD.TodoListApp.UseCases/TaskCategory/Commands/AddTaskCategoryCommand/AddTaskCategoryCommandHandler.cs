using MediatR;
using AutoMapper;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.TaskCategory.Commands.AddTaskCategoryCommand;

/// <summary>
/// Обработчик команды добавления категории задачи.
/// </summary>
public class AddTaskCategoryCommandHandler(ITaskCategoryRepository taskCategoryRepository,
                                           IMapper mapper)
: IRequestHandler<AddTaskCategoryCommand, Result<TaskCategoryModel>>
{
    private readonly ITaskCategoryRepository _taskCategoryRepository = taskCategoryRepository 
        ?? throw new ArgumentNullException(nameof(taskCategoryRepository));

    private readonly IMapper _mapper = mapper 
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<TaskCategoryModel>> Handle(AddTaskCategoryCommand request, CancellationToken cancellationToken)
    {
        var taskCategory = _mapper.Map<Core.TaskCategory>(request.Model);
        taskCategory.UserId = request.UserId;

        await _taskCategoryRepository.Add(taskCategory);
        return Result<TaskCategoryModel>.SuccessfullyCreated(_mapper.Map<TaskCategoryModel>(taskCategory));
    }
}
