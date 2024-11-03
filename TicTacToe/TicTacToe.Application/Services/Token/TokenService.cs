using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Application.AutentificationOptions;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.TokenServices;

namespace TicTacToe.Application.Services.Token;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };

        var identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        // Добавьте код для генерации токена с использованием вашей JWT-конфигурации.
        // Примерный код:
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(JwtOptions.GetKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}