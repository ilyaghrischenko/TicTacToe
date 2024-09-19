using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.DbModelsServices;
using Mafia.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mafia.Application.Services.DbModels;

public class UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    
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

    public async Task ChangeLogin(string userLogin, string newLogin)
    {
        var me = await _userRepository.Get(userLogin);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }
        
        if (me.Login == newLogin)
        {
            throw new ArgumentException("New login is the same as old");
        }
        
        me.Login = newLogin;
        
        await _userRepository.Update(me);
    }
    public async Task ChangePassword(string userLogin, string newPassword)
    {
        var me = await _userRepository.Get(userLogin);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }
        
        if (me.Password == newPassword)
        {
            throw new ArgumentException("New password is the same as old");
        }
        
        me.Password = _passwordHasher.HashPassword(null, newPassword);
        
        await _userRepository.Update(me);
    }
    public async Task ChangeEmail(string userLogin, string newEmail)
    {
        var me = await _userRepository.Get(userLogin);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }
        
        if (me.Email == newEmail)
        {
            throw new ArgumentException("New email is the same as old");
        }
        
        me.Email = newEmail;
        
        await _userRepository.Update(me);
    }
    public async Task ChangeAvatar(string userLogin, byte[] newAvatar)
    {
        var me = await _userRepository.Get(userLogin);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }
        
        if (me.Avatar == newAvatar)
        {
            throw new ArgumentException("New avatar is the same as old");
        }
        
        me.Avatar = newAvatar;
        
        await _userRepository.Update(me);
    }
}