using Mafia.DTO.Models;

namespace Mafia.Application.Interfaces;

public interface IAccountService
{
    public Task Login(LoginModel loginModel);
    public Task Register(RegisterModel registerModel);
}