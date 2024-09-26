using System.Security.Claims;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.DbModelsServices;
using Mafia.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace Mafia.Application.Services.Controllers;

public class FriendsControllerService(IUserService userService,
    IHttpContextAccessor httpContextAccessor) : IFriendsControllerService
{
    private readonly IUserService _userService = userService;
    private readonly int _userId = int.Parse(
        httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

    public async Task AddFriend(int newFriendId)
    {
        await _userService.AddFriend(_userId, newFriendId);
    }

    public async Task DeleteFriend(int friendId)
    {
        await _userService.DeleteFriend(_userId, friendId);
    }
}