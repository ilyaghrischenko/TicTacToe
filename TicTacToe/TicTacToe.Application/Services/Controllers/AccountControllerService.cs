using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.Domain.Interfaces.Repositories;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Application.AutentificationOptions;
using TicTacToe.Domain.Interfaces.DbModelsServices;

namespace TicTacToe.Application.Services.Controllers;

public class AccountControllerService(
    IUserService userService,
    IPasswordHasher<User> passwordHasher) : IAccountControllerService
{
    private readonly IUserService _userService = userService;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    
    public async Task<object> Login(LoginModel loginModel)
    {
        try
        {
            var user = await _userService.Login(loginModel);
            
            var identity = GetIdentity(user);
            var token = GetToken(identity);
            var response = new
            {
                access_token = token,
                user_role = user.Role.ToString()
            };
            
            return response;
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task Register(RegisterModel registerModel)
    {
        try
        {
            await _userService.Register(registerModel);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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