using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Application.Services.DbModels;

public class UserService(
    IUserRepository userRepository,
    IFriendRepository friendRepository,
    IPasswordHasher<User> passwordHasher) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IFriendRepository _friendRepository = friendRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task AddFriend(int userId, int friendId)
    {
        var me = await _userRepository.Get(userId);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        var friendToAdd = await _userRepository.Get(friendId);
        if (friendToAdd == null)
        {
            throw new KeyNotFoundException("New friend not found");
        }

        var meFriends = await _friendRepository.GetUserFriends(me.Id);
        var friendToAddFriends = await _friendRepository.GetUserFriends(friendToAdd.Id);

        if (meFriends.Any(x => x.Id == friendToAdd.Id))
            throw new ArgumentException("You already have this friend");

        if (friendToAddFriends.Any(x => x.Id == me.Id))
            throw new ArgumentException("This user already has you as a friend");

        await _friendRepository.Add(new Friend(me, friendToAdd));
        await _friendRepository.Add(new Friend(friendToAdd, me));
    }

    public async Task DeleteFriend(int userId, int friendId)
    {
        var me = await _userRepository.Get(userId);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        var friendToRemove = await _userRepository.Get(friendId);
        if (friendToRemove == null)
        {
            throw new KeyNotFoundException("Friend not found");
        }

        var meFrend = await _friendRepository.Get(me.Id, friendToRemove.Id);
        var friendToRemoveFriend = await _friendRepository.Get(friendToRemove.Id, me.Id);


        if (meFrend == null)
            throw new ArgumentException("This friend is not in your friends list");

        if (friendToRemoveFriend == null)
            throw new ArgumentException("This user already hasn't you as a friend");

        await _friendRepository.Delete(meFrend.Id);
        await _friendRepository.Delete(friendToRemoveFriend.Id);
    }

    public async Task ChangeLogin(string userId, string newLogin)
    {
        var me = await _userRepository.Get(int.Parse(userId));
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        if (me.Login == newLogin)
        {
            throw new ArgumentException("New login is the same as old");
        }

        if (await _userRepository.Get(newLogin) != null)
        {
            throw new ArgumentException("New login already exists");
        }

        await _userRepository.Update(me, () => { me.Login = newLogin; });
    }

    public async Task ChangePassword(string userId, string newPassword)
    {
        var me = await _userRepository.Get(int.Parse(userId));
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        if (me.Password == newPassword)
        {
            throw new ArgumentException("New password is the same as old");
        }


        await _userRepository.Update(me, () => { me.Password = _passwordHasher.HashPassword(null, newPassword); });
    }

    public async Task ChangeEmail(string userId, string newEmail)
    {
        var me = await _userRepository.Get(int.Parse(userId));
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        if (me.Email == newEmail)
        {
            throw new ArgumentException("New email is the same as old");
        }

        await _userRepository.Update(me, () => { me.Email = newEmail; });
    }

    public async Task ChangeAvatar(string userId, ChangeAvatarModel changeAvatarModel)
    {
        var me = await _userRepository.Get(int.Parse(userId));
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }


        if (me.Avatar == changeAvatarModel.Avatar)
        {
            throw new ArgumentException("New avatar is the same as old");
        }


        await _userRepository.Update(me, () => { me.Avatar = changeAvatarModel.Avatar; });
    }
}