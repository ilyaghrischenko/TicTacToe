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
        var user = await _userRepository.Get(loginModel.login);
        if (user == null) throw new KeyNotFoundException("User not found");
            
        if (user.Password != loginModel.password) throw new ArgumentException("Wrong password");
    }

    public async Task Register(RegisterModel registerModel)
    {
        var user = await _userRepository.Get(registerModel.login);
        if (user != null) throw new ArgumentException("User already exists");
            
        user = new User(registerModel.login, registerModel.password, registerModel.email, null, null);
        await _userRepository.Add(user);
    }
}