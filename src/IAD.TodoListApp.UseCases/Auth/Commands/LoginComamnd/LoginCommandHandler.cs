using MediatR;
using AutoMapper;
using Microsoft.Extensions.Options;

using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Core.Services;
using IAD.TodoListApp.Core;
using IAD.TodoListApp.Packages;
using IAD.TodoListApp.UseCases.Abstractions;
using IAD.TodoListApp.UseCases.User;
using IAD.TodoListApp.Core.Options;

namespace IAD.TodoListApp.UseCases.Auth.Commands.LoginComamnd;

/// <summary>
/// Обработчик команды для авторизации пользователя.
/// </summary>
public class LoginCommandHandler(IUserRepository userRepository,
                                 ITokenService tokenService,
                                 IOptions<PasswordOptions> passwordOptions,
                                 IMapper mapper) 
    : IRequestHandler<LoginCommand, Result<TokenModel>>
{
    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    private readonly ITokenService _tokenService = tokenService
        ?? throw new ArgumentNullException(nameof(tokenService));

    private readonly string _salt = passwordOptions?.Value?.Salt
        ?? throw new ArgumentNullException(nameof(passwordOptions));

    public async Task<Result<TokenModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
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