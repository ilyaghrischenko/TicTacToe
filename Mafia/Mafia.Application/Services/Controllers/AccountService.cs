using Mafia.Application.Interfaces;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Repositories;
using Mafia.DTO.Models;
using Microsoft.AspNetCore.Identity;

namespace Mafia.Application.Services.Controllers;

//TODO: create services and implement them to controller, there should be IUserService
public class AccountService
    (IUserRepository userRepository, 
        PasswordHasher<User> passwordHasher) : IAccountService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly PasswordHasher<User> _passwordHasher = passwordHasher;
    
    public async Task Login(LoginModel loginModel)
    {
        var user = await _userRepository.Get(loginModel.Login);
        if (user == null) throw new KeyNotFoundException("User not found");
            
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginModel.Password);
        if (result == PasswordVerificationResult.Failed) throw new ArgumentException("Wrong password");
    }

    public async Task Register(RegisterModel registerModel)
    {
        var user = await _userRepository.Get(registerModel.Login);
        if (user != null) throw new ArgumentException("User already exists");

        user = new User
        {
            Login = registerModel.Login,
            Email = registerModel.Email,
            Password = _passwordHasher.HashPassword(null, registerModel.Password)
        };
        await _userRepository.Add(user);
    }
}