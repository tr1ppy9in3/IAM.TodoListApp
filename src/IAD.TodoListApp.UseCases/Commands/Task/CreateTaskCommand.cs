using MediatR;
using AutoMapper;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions.Repositories;
using IAD.TodoListApp.Core;

namespace IAD.TodoListApp.UseCases.Commands.Task;

public record CreateTaskCommand(long UserId, TaskInputModel Model) : IRequest<Result<TaskModel>>;

public class CreateTaskCommandHandler(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper) : IRequestHandler<CreateTaskCommand, Result<TaskModel>>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<TaskModel>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetById(request.UserId);

        if (user is null)
        {
            return Result<TaskModel>.Invalid($"Unable to find user with Id {request.UserId}");
        }

        var task = _mapper.Map<TodoTask>(request.Model);
        task.UserId = request.UserId;

        await _taskRepository.Add(task);

        return Result<TaskModel>.SuccessfullyCreated(_mapper.Map<TaskModel>(task));
    }
}
