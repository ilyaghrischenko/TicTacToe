using System.Security.Claims;
using TicTacToe.Domain.DbModels;
using Microsoft.AspNetCore.Http;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Contracts.DbModelsServices;

namespace TicTacToe.Application.Services.Controllers;

public class FriendsControllerService(
    IUserService userService,
    IHttpContextAccessor httpContextAccessor) : IFriendsControllerService
{
    private readonly IUserService _userService = userService;

    private readonly int _userId = int.Parse(
        httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

    public async Task AddFriendAsync(int newFriendId)
    {
        await _userService.AddFriendAsync(_userId, newFriendId);
    }

    public async Task DeleteFriendAsync(int friendId)
    {
        await _userService.DeleteFriendAsync(_userId, friendId);
    }
}