using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Repositories;

public interface IFriendRepository : IRepository<Friend>
{
    public Task<List<User>?> GetUserFriendsAsync(int userId);
    public Task<Friend?> GetAsync(int userId, int friendId);
}