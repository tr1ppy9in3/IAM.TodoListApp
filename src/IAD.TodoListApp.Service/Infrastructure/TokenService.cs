using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using IAD.TodoListApp.Core.Options;
using IAD.TodoListApp.UseCases.Abstractions;
using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core;
using AutoMapper;
using IAD.TodoListApp.UseCases.Auth;
using IAD.TodoListApp.Contracts;

namespace IAD.TodoListApp.Service.Infrastructure;

/// <summary>
/// Реализация <see cref="ITokenService"/>.
/// </summary>
public class TokenService(IOptions<JwtOptions> jwtOptions, ITokenRepository tokenRepository) : ITokenService
{
    /// <summary>
    /// Параметры JWT токена.
    /// </summary>
    private readonly JwtOptions _jwtOptions = jwtOptions.Value 
        ?? throw new ArgumentNullException(nameof(jwtOptions));

    /// <summary>
    /// Репозиторий для взаимодействия с токенами в базе данных.
    /// </summary>
    private readonly ITokenRepository _tokenRepository = tokenRepository 
        ?? throw new ArgumentNullException(nameof(tokenRepository));

    /// <inheritdoc/>
    public async Task<Token> GenerateToken(UserBase user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresTime = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiresTime,
            signingCredentials: creds);

        var tokenModel = new Token
        {
            Value = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = user.Id,
            Username = user.Login,
            ExpiresAt = expiresTime,
        };

        await _tokenRepository.Create(tokenModel);
        return tokenModel;
    }
}