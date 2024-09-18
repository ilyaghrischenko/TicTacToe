using System.Security.Claims;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace Mafia.Application.Services.Controllers;

public class FriendsControllerService(IUserRepository userRepository,
    IRepository<Friend> friendRepository,
    IHttpContextAccessor httpContextAccessor) : IFriendsControllerService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRepository<Friend> _friendRepository = friendRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<bool> AddFriend(int newFriendId)
    {
        var userLogin = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var user = await _userRepository.Get(userLogin);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var chosenUser = await _userRepository.Get(newFriendId);
        if (chosenUser == null)
        {
            throw new KeyNotFoundException("User not found");
        }
    
        //TODO: FINISH
        return true;
    }

    public async Task<bool> DeleteFriend(int friendId)
    {
        throw new NotImplementedException();
    }
}