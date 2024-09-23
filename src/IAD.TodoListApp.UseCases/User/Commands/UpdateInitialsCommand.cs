using MediatR;
using FluentValidation;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.UseCases.User.Models;

namespace IAD.TodoListApp.UseCases.User.Commands;

/// <summary>
/// Команда обновления инициалов пользователя.
/// </summary>
/// <param name="Id"> Идентификатор пользователя </param>
/// <param name="Model"> Модель инициалов. </param>
public record class UpdateInitialsCommand(long Id, UserInitialsModel Model) : IRequest<Result<Unit>>;

/// <summary>
/// Обработчик команды обновления инициалов пользователя.
/// </summary>
public class UpdateInitialsCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateInitialsCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<Unit>> Handle(UpdateInitialsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user is null)
        {
            return Result<Unit>.Invalid($"User with Id {request.Id} not found");
        }

        if (user is not RegularUser regularUser)
        {
            return Result<Unit>.Invalid($"User role doesn't allow to interact with profile picture!");
        }

        regularUser.Name = request.Model.Name;
        regularUser.Surname = request.Model.Surname;

        await _userRepository.Update(regularUser);

        return Result<Unit>.Empty();
    }
}

/// <summary>
/// Валидатор команды обновления инициалов пользователя.
/// </summary>
public class UpdateInitialsCommandValidator : AbstractValidator<UpdateInitialsCommand>
{
    public UpdateInitialsCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository))
            .SetValidator(new IsRegularUserValidator(userRepository));

        RuleFor(x => x.Model)
            .NotNull().WithMessage("Model is required!")
            .SetValidator(new UserInitialsModelValidator());
    }
}