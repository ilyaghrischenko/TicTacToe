using TicTacToe.Domain.DbModels;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Domain.Interfaces.DbModelsServices;

public interface IUserService
{
    public Task AddFriend(int userId, int friendId);
    public Task DeleteFriend(int userId, int friendId);
    public Task ChangeLogin(string userId, string newLogin);
    public Task ChangePassword(string userId, string newPassword);
    public Task ChangeEmail(string userId, string newEmail);
    public Task ChangeAvatar(string userId, ChangeAvatarModel changeAvatarModel);
    public Task<User> Login(LoginModel loginModel);
    public Task Register(RegisterModel registerModel);
    public Task<User> GetUserById(int userId);
    public Task<List<User>> GetAllUsers(int currentUserId);
    public Task<List<User>> GetFriends(int userId);
}