using TicTacToe.Domain.DbModels;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.DbModelsServices;

public interface IUserService
{
    public Task AddFriendAsync(int userId, int friendId);
    public Task DeleteFriendAsync(int userId, int friendId);
    public Task ChangeLoginAsync(string userId, string newLogin);
    public Task ChangePasswordAsync(string userId, string newPassword);
    public Task ChangeEmailAsync(string userId, string newEmail);
    public Task ChangeAvatarAsync(string userId, ChangeAvatarModel changeAvatarModel);
    public Task<User> LoginAsync(LoginModel loginModel);
    public Task RegisterAsync(RegisterModel registerModel);
    public Task<User> GetUserByIdAsync(int userId);
    public Task<List<User>> GetAllUsersAsync(int currentUserId);
    public Task<List<User>> GetFriendsAsync(int userId);
}