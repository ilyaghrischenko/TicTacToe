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

namespace TicTacToe.Application.Services.Controllers;

//TODO: create services and implement them to controller, there should be IUserService
public class AccountControllerService
    (IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher) : IAccountControllerService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    
    public async Task<object> Login(LoginModel loginModel)
    {
        var user = await _userRepository.Get(loginModel.Login);
        if (user == null) throw new KeyNotFoundException("User not found");
            
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginModel.Password);
        if (result == PasswordVerificationResult.Failed) throw new ArgumentException("Wrong password");
        
        var identity = GetIdentity(user);
        var token = GetToken(identity);
        var response = new
        {
            access_token = token,
        };
        return response;
    }

    public async Task Register(RegisterModel registerModel)
    {
        var user = await _userRepository.Get(registerModel.Login);
        if (user != null) throw new ArgumentException("User already exists");
        
        user = new User
        {
            Login = registerModel.Login,
            Email = registerModel.Email,
            Password = _passwordHasher.HashPassword(null, registerModel.Password),
        };
        await _userRepository.Add(user);
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