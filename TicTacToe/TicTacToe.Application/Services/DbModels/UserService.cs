using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Services.DbModels;

public class UserService(
    IUserRepository userRepository,
    IFriendRepository friendRepository,
    IPasswordHasher<User> passwordHasher) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IFriendRepository _friendRepository = friendRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task AddFriendAsync(int userId, int friendId)
    {
        var me = await _userRepository.GetAsync(userId);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        var friendToAdd = await _userRepository.GetAsync(friendId);
        if (friendToAdd == null)
        {
            throw new KeyNotFoundException("New friend not found");
        }

        var meFriends = await _friendRepository.GetUserFriendsAsync(me.Id);
        var friendToAddFriends = await _friendRepository.GetUserFriendsAsync(friendToAdd.Id);

        if (meFriends.Any(x => x.Id == friendToAdd.Id))
        {
            throw new ArgumentException("You already have this friend");
        }

        if (friendToAddFriends.Any(x => x.Id == me.Id))
        {
            throw new ArgumentException("This user already has you as a friend");
        }

        await _friendRepository.AddAsync(new Friend(me, friendToAdd));
        await _friendRepository.AddAsync(new Friend(friendToAdd, me));
    }

    public async Task DeleteFriendAsync(int userId, int friendId)
    {
        var me = await _userRepository.GetAsync(userId);
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        var friendToRemove = await _userRepository.GetAsync(friendId);
        if (friendToRemove == null)
        {
            throw new KeyNotFoundException("Friend not found");
        }

        var meFrend = await _friendRepository.GetAsync(me.Id, friendToRemove.Id);
        var friendToRemoveFriend = await _friendRepository.GetAsync(friendToRemove.Id, me.Id);

        if (meFrend == null)
        {
            throw new ArgumentException("This friend is not in your friends list");
        }

        if (friendToRemoveFriend == null)
        {
            throw new ArgumentException("This user already hasn't you as a friend");
        }

        await _friendRepository.DeleteAsync(meFrend.Id);
        await _friendRepository.DeleteAsync(friendToRemoveFriend.Id);
    }

    public async Task ChangeLoginAsync(string userId, string newLogin)
    {
        var me = await _userRepository.GetAsync(int.Parse(userId));
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        if (me.Login == newLogin)
        {
            throw new ArgumentException("New login is the same as old");
        }

        if (await _userRepository.GetAsync(newLogin) != null)
        {
            throw new ArgumentException("New login already exists");
        }

        await _userRepository.UpdateAsync(me, () =>
        {
            me.Login = newLogin;
        });
    }

    public async Task ChangePasswordAsync(string userId, string newPassword)
    {
        var me = await _userRepository.GetAsync(int.Parse(userId));
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        if (me.Password == newPassword)
        {
            throw new ArgumentException("New password is the same as old");
        }
        
        await _userRepository.UpdateAsync(me, () =>
        {
            me.Password = _passwordHasher.HashPassword(null, newPassword);
        });
    }

    public async Task ChangeEmailAsync(string userId, string newEmail)
    {
        var me = await _userRepository.GetAsync(int.Parse(userId));
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }

        if (me.Email == newEmail)
        {
            throw new ArgumentException("New email is the same as old");
        }

        await _userRepository.UpdateAsync(me, () =>
        {
            me.Email = newEmail;
        });
    }

    public async Task ChangeAvatarAsync(string userId, ChangeAvatarModel changeAvatarModel)
    {
        var me = await _userRepository.GetAsync(int.Parse(userId));
        if (me == null)
        {
            throw new KeyNotFoundException("You not found");
        }
        
        if (me.Avatar == changeAvatarModel.Avatar)
        {
            throw new ArgumentException("New avatar is the same as old");
        }
        
        await _userRepository.UpdateAsync(me, () =>
        {
            me.Avatar = changeAvatarModel.Avatar;
        });
    }

    public async Task<User> LoginAsync(LoginModel loginModel)
    {
        var user = await _userRepository.GetAsync(loginModel.Login);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        if (user.Login != loginModel.Login)
        {
            throw new KeyNotFoundException("User not found");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginModel.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new ArgumentException("Wrong password");
        }
        
        return user;
    }

    public async Task RegisterAsync(RegisterModel registerModel)
    {
        var user = await _userRepository.GetAsync(registerModel.Login);
        if (user != null)
        {
            throw new ArgumentException("User already exists");
        }

        var admin = await _userRepository.GetAdminAsync();
        Role role = Role.User;
        if (admin != null)
        {
            if (registerModel.Login == admin.Login && registerModel.Password == admin.Password)
            {
                role = Role.Admin;
            }
        }

        user = new User
        {
            Login = registerModel.Login,
            Email = registerModel.Email,
            Password = _passwordHasher.HashPassword(null, registerModel.Password),
            Role = role
        };
        await _userRepository.AddAsync(user);
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        return user;
    }

    public async Task<List<User>> GetAllUsersAsync(int currentUserId)
    {
        var allUsers = await _userRepository.GetAllAsync();
        if (allUsers == null || allUsers.Count == 0)
        {
            return new List<User>();
        }

        allUsers.RemoveAll(user => user.Id == currentUserId
                                   || user.Role == Role.Admin);

        return allUsers;
    }

    public async Task<List<User>> GetFriendsAsync(int userId)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        var userFriends = await _friendRepository.GetUserFriendsAsync(user.Id);
        return userFriends ?? new List<User>();
    }
}