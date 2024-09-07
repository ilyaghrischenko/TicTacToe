using Domain.DbModels;
using Domain.Interfaces.Repositories;
using DTO.Models;
using Service.Interfaces;

namespace Service.Services;

//TODO: create services and implement them to controller, there should be IUserService
public class AccountService(IUserRepository userRepository) : IAccountService
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task Login(LoginModel loginModel)
    {
        var user = await _userRepository.Get(loginModel.Login);
        if (user == null) throw new KeyNotFoundException("User not found");
            
        if (user.Password != loginModel.Password) throw new ArgumentException("Wrong password");
    }

    public async Task Register(RegisterModel registerModel)
    {
        var user = await _userRepository.Get(registerModel.Login);
        if (user != null) throw new ArgumentException("User already exists");
            
        user = new User(registerModel.Login, registerModel.Password, registerModel.Email, null, null);
        await _userRepository.Add(user);
    }
}