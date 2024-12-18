using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicTacToe.Domain.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Application.AutentificationOptions;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.DTO.Requests;

namespace TicTacToe.Application.Services.Controllers;

public class AccountControllerService(
    IUserService userService,
    IPasswordHasher<User> passwordHasher) : IAccountControllerService
{
    private readonly IUserService _userService = userService;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task<object> LoginAsync(LogInRequest logInRequest)
    {
        var user = await _userService.LoginAsync(logInRequest);

        var identity = GetIdentity(user);
        var token = GetToken(identity);
        var response = new
        {
            access_token = token,
            user_role = user.Role.ToString(),
            user_login = user.Login
        };

        return response;
    }

    public async Task RegisterAsync(RegisterRequest registerRequest)
    {
        await _userService.RegisterAsync(registerRequest);
    }

    public ClaimsIdentity GetIdentity(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        return identity;
    }

    public string? GetToken(ClaimsIdentity identity)
    {
        var timeNow = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: JwtOptions.ISSUER,
            audience: JwtOptions.AUDIENCE,
            notBefore: timeNow,
            claims: identity.Claims,
            expires: timeNow.Add(TimeSpan.FromDays(JwtOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(JwtOptions.GetKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}