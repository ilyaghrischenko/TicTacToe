using System.Security.Claims;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Mafia.Application.Services.Controllers;

public class UserControllerService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : IUserControllerService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public async Task<User> GetUser()
    {
        var userLogin = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var user = await _userRepository.Get(userLogin);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        
        return user;
    }
}