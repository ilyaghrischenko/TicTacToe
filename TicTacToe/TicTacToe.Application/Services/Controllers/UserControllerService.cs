using System.Security.Claims;
using TicTacToe.Domain.DbModels;
using Microsoft.AspNetCore.Http;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Contracts.DbModelsServices;
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

    public async Task<User> GetUserAsync()
    {
        var userId = GetCurrentUserId();
        var user = await _userService.GetUserByIdAsync(userId);
        if (user.Role == Role.Blocked)
        {
            throw new UnauthorizedAccessException("User is blocked");
        }

        return user;
    }

    public async Task<Statistic> GetUserStatisticsAsync()
    {
        var userId = GetCurrentUserId();
        var userStatistics = await _statisticService.GetUserStatisticsAsync(userId);
        return userStatistics;
    }

    public async Task<List<User>?> GetFriendsAsync()
    {
        var userId = GetCurrentUserId();
        var userFriends = await _userService.GetFriendsAsync(userId);
        return userFriends;
    }

    public async Task<List<User>?> GetAllUsersAsync()
    {
        var userId = GetCurrentUserId();
        var allUsers = await _userService.GetAllUsersAsync(userId);
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