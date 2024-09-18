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
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<bool> AddFriend(int newFriendId)
    {
        //TODO: FINISH
        
        // var userLogin = _httpContextAccessor.HttpContext.User.Claims
        //     .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        // var user = await _userRepository.Get(userLogin);
        // if (user == null)
        // {
        //     throw new KeyNotFoundException("User not found");
        // }
        //
        // var friend = await _userRepository.Get(newFriendId);
        //
        // try
        // {
        //     return await _userService.AddFriend(user, friend);
        // }
        return true;
    }

    public async Task<bool> DeleteFriend(int friendId)
    {
        throw new NotImplementedException();
    }
}