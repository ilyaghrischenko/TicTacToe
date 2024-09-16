using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Mafia.Domain.DbModels;
using Mafia.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mafia.Domain.Interfaces.Controllers;

public interface IAccountControllerService
{
    public Task<object> Login(LoginModel loginModel);
    public Task Register(RegisterModel registerModel);
    public ClaimsIdentity GetIdentity(User user);
    public string? GetToken(ClaimsIdentity identity);
}