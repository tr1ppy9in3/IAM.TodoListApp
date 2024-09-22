using MediatR;
using Microsoft.Extensions.Options;

using IAD.TodoListApp.Core.Options;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.Core;
using IAD.TodoListApp.Core.Services;
using IAD.TodoListApp.UseCases.Abstractions;
using IAD.TodoListApp.UseCases.User;
using IAD.TodoListApp.Contracts;
using AutoMapper;

namespace IAD.TodoListApp.UseCases.Auth.Commands.LoginCommand;

/// <summary>
/// Команда для авторизации пользователя.
/// </summary>
/// <param name="Login"> Логин пользователя. </param>
/// <param name="Password"> Пароль пользователя. </param>
/// <returns> Токен авторизованного пользователя. </returns>
public sealed record class LoginCommand(string Login, string Password) : IRequest<Result<TokenModel>>;

/// <summary>
/// Обработчик команды для авторизации пользователя.
/// </summary>
public class LoginCommandHandler(IUserRepository userRepository,
                                 ITokenService tokenService,
                                 ITokenRepository tokenRepository,
                                 IOptions<PasswordOptions> passwordOptions,
                                 IMapper mapper) : IRequestHandler<LoginCommand, Result<TokenModel>>
{
    private readonly IMapper _mapper = mapper 
        ?? throw new ArgumentNullException(nameof(mapper));

    private readonly IUserRepository _userRepository = userRepository 
        ?? throw new ArgumentNullException(nameof(userRepository));
    
    private readonly ITokenService _tokenService = tokenService 
        ?? throw new ArgumentNullException(nameof(tokenService));
    
    private readonly ITokenRepository _tokenRepository = tokenRepository 
        ?? throw new ArgumentNullException(nameof(tokenRepository));
    
    private readonly string _salt = passwordOptions?.Value?.Salt 
        ?? throw new ArgumentNullException(nameof(passwordOptions));

    public async Task<Result<TokenModel>> Handle(LoginCommand request, CancellationToken _)
    {
        var user = await _userRepository.Resolve(request.Login, CryptographyService.HashPassword(request.Password, _salt));

        if (user is null)
        {
            return Result<TokenModel>.Invalid("Invalid password or login");
        }

        Token token = await _tokenService.GenerateToken(user);
        return Result<TokenModel>.Success(_mapper.Map<TokenModel>(token));
    }
}
