using MediatR;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.TaskCategory.Models;
using FluentValidation;


namespace IAD.TodoListApp.UseCases.TaskCategory.Commands;

public record class AddTaskCategoryCommand(long UserId, TaskCategoryInputModel Model) : IRequest<Result<TaskCategoryModel>>;

public class AddTaskCategoryCommandHandler() : IRequestHandler<AddTaskCategoryCommand, Result<TaskCategoryModel>>
{
    public Task<Result<TaskCategoryModel>> Handle(AddTaskCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class AddTaskCategoryCommandValidator : AbstractValidator<AddTaskCategoryCommand>
{
    public AddTaskCategoryCommandValidator()
    {

    }
}
