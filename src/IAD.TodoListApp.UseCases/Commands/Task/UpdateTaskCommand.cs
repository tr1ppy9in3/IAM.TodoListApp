using AutoMapper;
using MediatR;

using IAD.TodoListApp.Core;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;

namespace IAD.TodoListApp.UseCases.Commands.Task;

public record UpdateTaskCommand(TaskInputModel Model) : IRequest<Result<Unit>>;

public class UpdateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper) : IRequestHandler<UpdateTaskCommand, Result<Unit>>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken _)
    {
        var task = _mapper.Map<TodoTask>(request.Model);
        await _taskRepository.Update(task);

        return Result<Unit>.Empty();
    }
}
