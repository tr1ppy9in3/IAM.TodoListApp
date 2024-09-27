using AutoMapper;
using MediatR;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.TaskCategory.Commands.UpdateTaskCategoryCommand;

/// <summary>
/// Обработчик команды обновления категории задач.
/// </summary>
public class UpdateTaskCategoryCommandHandler(ITaskCategoryRepository taskCategoryRepository,
                                      IMapper mapper) : IRequestHandler<UpdateTaskCategoryCommand, Result<Unit>>
{
    private readonly ITaskCategoryRepository _taskCategoryRepository = taskCategoryRepository
        ?? throw new ArgumentNullException(nameof(taskCategoryRepository));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<Unit>> Handle(UpdateTaskCategoryCommand request, CancellationToken cancellationToken)
    {
        var taskCategory = _mapper.Map<Core.TaskCategory>(request.Model);

        taskCategory.Id = request.TaskCategoryId;
        taskCategory.UserId = request.UserId;

        await _taskCategoryRepository.Update(taskCategory);
        return Result<Unit>.Empty();
    }
}