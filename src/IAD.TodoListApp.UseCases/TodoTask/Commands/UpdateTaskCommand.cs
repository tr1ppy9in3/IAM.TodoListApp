using MediatR;
using AutoMapper;
using FluentValidation;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TodoTask.Models;

namespace IAD.TodoListApp.UseCases.TodoTask.Commands;

/// <summary>
/// Команда обновления задачи.
/// </summary>
/// <param name="Model"> Модель задачи.</param>
public record UpdateTaskCommand(TaskInputModel Model) : IRequest<Result<Unit>>;

/// <summary>
/// Обработчик команды обновления задачи.
/// </summary>
public class UpdateTaskCommandHandler(ITaskRepository taskRepository, 
                                      IMapper mapper) : IRequestHandler<UpdateTaskCommand, Result<Unit>>
{
    private readonly ITaskRepository _taskRepository = taskRepository 
        ?? throw new ArgumentNullException(nameof(taskRepository));

    private readonly IMapper _mapper = mapper 
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken _)
    {
        var task = _mapper.Map<Core.TodoTask>(request.Model);
        await _taskRepository.Update(task);

        return Result<Unit>.Empty();
    }
}

/// <summary>
/// Валидатор команды обновления задачи.
/// </summary>
public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Task model is required!")
            .SetValidator(new TaskInputModelValidator());
    }
}
