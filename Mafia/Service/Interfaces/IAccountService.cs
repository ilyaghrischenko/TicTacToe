using DTO.Models;

namespace Service.Interfaces;

public interface IAccountService
{
    public Task Login(LoginModel loginModel);
    public Task Register(RegisterModel registerModel);
}