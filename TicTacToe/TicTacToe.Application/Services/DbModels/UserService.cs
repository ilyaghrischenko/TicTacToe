using TicTacToe.Domain.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Application.Exceptions;
using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Requests;

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
        var meTask = _userRepository.GetAsync(userId);
        var friendToAddTask = _userRepository.GetAsync(friendId);
        
        await Task.WhenAll(meTask, friendToAddTask);
        
        var me = await meTask;
        var friendToAdd = await friendToAddTask;
        
        if (me == null)
        {
            throw new EntityNotFoundException("You not found");
        }
        if (friendToAdd == null)
        {
            throw new EntityNotFoundException("New friend not found");
        }
        
        var meFriendsTask = _friendRepository.GetUserFriendsAsync(me.Id);
        var friendToAddFriendsTask = _friendRepository.GetUserFriendsAsync(friendToAdd.Id);
        
        await Task.WhenAll(meFriendsTask, friendToAddFriendsTask);
        
        var meFriends = await meFriendsTask;
        var friendToAddFriends = await friendToAddFriendsTask;
        
        if (meFriends.Any(x => x.Id == friendToAdd.Id))
        {
            throw new ArgumentException("You already have this friend");
        }
        if (friendToAddFriends.Any(x => x.Id == me.Id))
        {
            throw new ArgumentException("This user already has you as a friend");
        }

        var addFriendToMeTask = _friendRepository.AddAsync(me.Id, friendToAdd.Id);
        var addFriendToUserTask = _friendRepository.AddAsync(friendToAdd.Id, me.Id);
        
        await Task.WhenAll(addFriendToMeTask, addFriendToUserTask);
    }

    public async Task DeleteFriendAsync(int userId, int friendId)
    {
        var meTask = _userRepository.GetAsync(userId);
        var friendToRemoveTask = _userRepository.GetAsync(friendId);
        
        await Task.WhenAll(meTask, friendToRemoveTask);
        
        var me = await meTask;
        var friendToRemove = await friendToRemoveTask;

        if (me == null)
        {
            throw new EntityNotFoundException("You not found");
        }
        if (friendToRemove == null)
        {
            throw new EntityNotFoundException("Friend not found");
        }

        var meFriendTask = _friendRepository.GetAsync(me.Id, friendToRemove.Id);
        var friendToRemoveFriendTask = _friendRepository.GetAsync(friendToRemove.Id, me.Id);
        
        await Task.WhenAll(meFriendTask, friendToRemoveFriendTask);
        
        var meFriend = await meFriendTask;
        var friendToRemoveFriend = await friendToRemoveFriendTask;

        if (meFriend == null)
        {
            throw new ArgumentException("This user is not in your friends list");
        }
        if (friendToRemoveFriend == null)
        {
            throw new ArgumentException("Friendship does not exist");
        }

        var deleteMyFriendTask = _friendRepository.DeleteAsync(meFriend.Id);
        var deleteUserFriendTask = _friendRepository.DeleteAsync(friendToRemoveFriend.Id);
        
        await Task.WhenAll(deleteMyFriendTask, deleteUserFriendTask);
    }

    public async Task ChangeLoginAsync(string userId, string newLogin)
    {
        var meTask = _userRepository.GetAsync(int.Parse(userId));
        var existingUserTask = _userRepository.GetAsync(newLogin);
        
        await Task.WhenAll(meTask, existingUserTask);
        
        var me = await meTask;
        var existingUser = await existingUserTask;
        
        if (me == null)
        {
            throw new EntityNotFoundException("You not found");
        }

        if (existingUser != null)
        {
            throw new ArgumentException("New login already exists");
        }
        
        if (me.Login == newLogin)
        {
            throw new OldDataException("New login is the same as old");
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
            throw new EntityNotFoundException("You not found");
        }

        if (me.Password == newPassword)
        {
            throw new OldDataException("New password is the same as old");
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
            throw new EntityNotFoundException("You not found");
        }

        if (me.Email == newEmail)
        {
            throw new OldDataException("New email is the same as old");
        }

        await _userRepository.UpdateAsync(me, () =>
        {
            me.Email = newEmail;
        });
    }

    public async Task ChangeAvatarAsync(string userId, ChangeAvatarRequest changeAvatarRequest)
    {
        var me = await _userRepository.GetAsync(int.Parse(userId));
        if (me == null)
        {
            throw new EntityNotFoundException("You not found");
        }
        
        if (me.Avatar == changeAvatarRequest.Avatar)
        {
            throw new OldDataException("New avatar is the same as old");
        }
        
        await _userRepository.UpdateAsync(me, () =>
        {
            me.Avatar = changeAvatarRequest.Avatar;
        });
    }

    public async Task<User> LoginAsync(LogInRequest logInRequest)
    {
        var user = await _userRepository.GetAsync(logInRequest.Login);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        if (user.Login != logInRequest.Login)
        {
            throw new EntityNotFoundException("User not found");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, logInRequest.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new ArgumentException("Wrong password");
        }
        
        return user;
    }

    public async Task RegisterAsync(RegisterRequest registerRequest)
    {
        var userTask = _userRepository.GetAsync(registerRequest.Login);
        var adminTask = _userRepository.GetAdminAsync();
        
        await Task.WhenAll(userTask, adminTask);
        
        var user = await userTask;
        var admin = await adminTask;
        
        if (user != null)
        {
            throw new ArgumentException("User with this login already exists");
        }

        Role role = Role.User;
        if (admin != null)
        {
            if (registerRequest.Login == admin.Login && registerRequest.Password == admin.Password)
            {
                role = Role.Admin;
            }
        }

        user = new User
        {
            Login = registerRequest.Login,
            Email = registerRequest.Email,
            Password = _passwordHasher.HashPassword(null, registerRequest.Password),
            Role = role
        };
        await _userRepository.AddAsync(user);
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        return user;
    }

    public async Task<List<User>> GetAllUsersAsync(int currentUserId)
    {
        var allUsers = await _userRepository.GetAllAsync() ?? new();

        allUsers.RemoveAll(user =>
                user.Id == currentUserId
                || user.Role == Role.Admin);

        return allUsers;
    }

    public async Task<List<User>> GetFriendsAsync(int userId)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        var userFriends = await _friendRepository.GetUserFriendsAsync(user.Id) ?? new();
        return userFriends;
    }
}