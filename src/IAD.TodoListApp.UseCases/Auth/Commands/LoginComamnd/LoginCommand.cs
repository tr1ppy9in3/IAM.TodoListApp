using FluentValidation;

using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Contracts;


namespace IAD.TodoListApp.UseCases.Auth.Commands.LoginComamnd;

/// <summary>
/// Команда для авторизации пользователя.
/// </summary>
/// <param name="Login"> Логин пользователя. </param>
/// <param name="Password"> Пароль пользователя. </param>
/// <returns> Токен авторизованного пользователя. </returns>
public sealed record LoginCommand(string Login, string Password) : IValidatableCommand<TokenModel>;

/// <summary>
/// Валидатор команды для авторизации.
/// </summary>
public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Login)
             .NotEmpty().WithMessage("Логин обязателен.")
             .Length(3, 50).WithMessage("Логин должен содержать от 3 до 50 символов.")
             .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Логин может содержать только алфавитно-цифровые символы и символы подчеркивания.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен.")
            .Length(8, 100).WithMessage("Пароль должен содержать от 8 до 100 символов.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{8,}$")
            .WithMessage("Пароль должен содержать хотя бы одну заглавную букву, одну строчную букву и одну цифру.");
    }
}