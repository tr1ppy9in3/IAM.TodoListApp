using IAD.TodoListApp.Packages;
using MediatR;

namespace IAD.TodoListApp.UseCases.Commands.Auth.RegistrationCommand;

/// <summary>
/// Команда для регистрации пользователя
/// </summary>
/// <param name="Login"> Логин пользователя</param>
/// <param name="Password"> Пароль пользователя</param>
/// <param name="Email"> Email пользователя. </param>
/// <param name="Role"> Роль пользователя.</param>
public sealed record class RegistrationCommand(string Login, string Password, string Email, string Role) : IRequest<Result<Unit>>;
