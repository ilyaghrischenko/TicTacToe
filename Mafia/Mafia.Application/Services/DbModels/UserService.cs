using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.DbModelsServices;
using Mafia.Domain.Interfaces.Repositories;

namespace Mafia.Application.Services.DbModels;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<bool> AddFriend(User user, User friend)
    {
        var me = await _userRepository.Get(user.Login);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }
        
        var friendToAdd = await _userRepository.Get(friend.Login);
        if (friendToAdd == null)
        {
            throw new KeyNotFoundException("New friend not found");
        }

        if (me.Friends.Contains(friendToAdd))
        {
            throw new ArgumentException("You already have this friend");
        }
        
        me.Friends.Add(friendToAdd);
        await _userRepository.Update(me);
        return true;
    }

    public async Task<bool> DeleteFriend(User user, User friend)
    {
        var me = await _userRepository.Get(user.Login);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }
        
        var friendToDelete = await _userRepository.Get(friend.Login);
        if (friendToDelete == null)
        {
            throw new KeyNotFoundException("Friend not found");
        }

        if (!me.Friends.Contains(friendToDelete))
        {
            throw new ArgumentException("You don't have this friend");
        }
        
        me.Friends.Remove(friendToDelete);
        await _userRepository.Update(me);
        return true;
    }
}