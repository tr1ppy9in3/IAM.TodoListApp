using AutoMapper;
using MediatR;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.UseCases.User;
using System.Runtime.CompilerServices;

namespace IAD.TodoListApp.UseCases.TaskCategory.Queries;

/// <summary>
/// Запрос на получение доступных для пользователя категорий задач.
/// </summary>
/// <param name="UserId"> Идентификатор пользователя. </param>
public record class GetAvailableTaskCategoriesQuery(long UserId) : IStreamRequest<TaskCategoryModel>;

/// <summary>
/// Обработчик запроса на полученых доступных для пользователя категорий задач.
/// </summary>
public class GetAvailableTaskCategoriesQueryHandler(ITaskCategoryRepository taskCategoryRepository,
                                           IUserRepository userRepository,
                                           IMapper mapper) : IStreamRequestHandler<GetAvailableTaskCategoriesQuery, TaskCategoryModel>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    private readonly ITaskCategoryRepository _taskCategoryRepository = taskCategoryRepository
        ?? throw new ArgumentNullException(nameof(taskCategoryRepository));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async IAsyncEnumerable<TaskCategoryModel> Handle(GetAvailableTaskCategoriesQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        if (user is not null)
        {
            await foreach (var taskCategory in _taskCategoryRepository.GetAllAvailable(request.UserId))
            {
                yield return _mapper.Map<TaskCategoryModel>(taskCategory);
            }

        }
    }
}