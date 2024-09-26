using System.Security.Claims;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Application.Services.Controllers;

public class UserControllerService(
    IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor,
    IUserService userService,
    IFriendRepository friendRepository) : IUserControllerService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUserService _userService = userService;
    private readonly IFriendRepository _friendRepository = friendRepository;
    
    public async Task<User> GetUser()
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var user = await _userRepository.Get(int.Parse(userId));
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        
        return user;
    }

    public async Task<List<User>?> GetFriends()
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var user = await _userRepository.Get(int.Parse(userId));
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        var userFriends = await _friendRepository.GetUserFriends(user.Id);
        if (userFriends == null || userFriends.Count == 0)
        {
            return new List<User>(); 
        }
        return userFriends;
    }

    public async Task<List<User>?> GetAllUsers()
    {
        // Получаем всех пользователей
        var allUsers = await _userRepository.GetAll();
    
        if (allUsers == null || allUsers.Count == 0)
        {
            return new List<User>(); // Возвращаем пустой список, если пользователей нет
        }

        // Получаем ID текущего пользователя
        var nameClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (nameClaim == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var currentUserId = int.Parse(nameClaim);

        // Удаляем текущего пользователя из списка
        allUsers.RemoveAll(x => x.Id == currentUserId);

        return allUsers; // Возвращаем список пользователей
    }
}