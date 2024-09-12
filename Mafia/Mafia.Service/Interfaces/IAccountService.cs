using Mafia.DTO.Models;

namespace Mafia.Service.Interfaces;

public interface IAccountService
{
    public Task Login(LoginModel loginModel);
    public Task Register(RegisterModel registerModel);
}