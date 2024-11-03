using System.Security.Claims;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Services.Controllers;

public class UserControllerService(
    IHttpContextAccessor httpContextAccessor,
    IUserService userService,
    IStatisticService statisticService) : IUserControllerService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUserService _userService = userService;
    private readonly IStatisticService _statisticService = statisticService;

    public async Task<User> GetUser()
    {
        var userId = GetCurrentUserId();
        var user = await _userService.GetUserById(userId);
        if (user.Role == Role.Blocked)
        {
            throw new UnauthorizedAccessException("User is blocked");
        }

        return user;
    }

    public async Task<Statistic> GetUserStatistics()
    {
        var userId = GetCurrentUserId();
        var userStatistics = await _statisticService.GetUserStatistics(userId);
        return userStatistics;
    }

    public async Task<List<User>?> GetFriends()
    {
        var userId = GetCurrentUserId();
        var userFriends = await _userService.GetFriends(userId);
        return userFriends;
    }

    public async Task<List<User>?> GetAllUsers()
    {
        var userId = GetCurrentUserId();
        var allUsers = await _userService.GetAllUsers(userId);
        return allUsers;
    }

    public int GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        return int.Parse(userIdClaim);
    }
}