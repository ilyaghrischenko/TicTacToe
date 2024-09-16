using Mafia.DTO.Models;

namespace Mafia.Domain.Interfaces.Controllers;

public interface IAccountControllerService
{
    public Task Login(LoginModel loginModel);
    public Task Register(RegisterModel registerModel);
}