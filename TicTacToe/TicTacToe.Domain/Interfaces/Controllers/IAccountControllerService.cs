using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.Controllers;

public interface IAccountControllerService : IBaseControllerService
{
    public Task<object> LoginAsync(LoginModel loginModel);
    public Task RegisterAsync(RegisterModel registerModel);
    public ClaimsIdentity GetIdentity(User user);
    public string? GetToken(ClaimsIdentity identity);
}