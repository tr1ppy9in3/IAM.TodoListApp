using MediatR;
using AutoMapper;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.UseCases.User;

namespace IAD.TodoListApp.UseCases.TodoTask.Queries;

/// <summary>
/// Запрос на получение доступных для пользователя задач.
/// </summary>
/// <param name="UserId"> Идентификатор пользователя. </param>
public record class GetAvailableTasksQuery(long UserId) : IStreamRequest<TaskModel>;

/// <summary>
/// Обработчик запроса на полученых доступных для пользователя задач.
/// </summary>
public class GetAvailableTasksQueryHandler(ITaskRepository taskRepository, 
                                           IUserRepository userRepository, 
                                           IMapper mapper) : IStreamRequestHandler<GetAvailableTasksQuery, TaskModel>
{
    private readonly IUserRepository _userRepository = userRepository 
        ?? throw new ArgumentNullException(nameof(userRepository));
    
    private readonly ITaskRepository _taskRepository = taskRepository 
        ?? throw new ArgumentNullException(nameof(taskRepository));
    
    private readonly IMapper _mapper = mapper 
        ?? throw new ArgumentNullException(nameof(mapper));

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