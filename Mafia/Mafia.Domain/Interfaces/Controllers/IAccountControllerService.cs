using Mafia.DTO.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mafia.Domain.Interfaces.Controllers;

public interface IAccountControllerService
{
    public Task Login(LoginModel loginModel);
    public Task Register(RegisterModel registerModel);
    public Task<List<ValidationError>> GetErrors(ModelStateDictionary modelState);
}