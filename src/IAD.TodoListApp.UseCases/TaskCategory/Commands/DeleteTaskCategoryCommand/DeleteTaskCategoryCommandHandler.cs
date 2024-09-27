using MediatR;

using IAD.TodoListApp.Packages;

namespace IAD.TodoListApp.UseCases.TaskCategory.Commands.DeleteTaskCategoryCommand;

/// <summary>
/// Обработчик команды удаления категории задачи.
/// </summary>
public class DeleteTaskCategoryCommandHandler(ITaskCategoryRepository taskCategoryRepository) : IRequestHandler<DeleteTaskCategoryCommand, Result<Unit>>
{
    private readonly ITaskCategoryRepository _taskCategoryRepository = taskCategoryRepository 
        ?? throw new ArgumentNullException(nameof(taskCategoryRepository));

    public async Task<Result<Unit>> Handle(DeleteTaskCategoryCommand request, CancellationToken cancellationToken)
    {
        await _taskCategoryRepository.Delete(request.TaskCategoryId);
        return Result<Unit>.Empty();
    }
}