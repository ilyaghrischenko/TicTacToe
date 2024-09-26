using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.Repositories;

public interface IFriendRepository : IRepository<Friend>
{
    public Task<List<User>?> GetUserFriends(int userId);
    public Task<Friend?> Get(int userId, int friendId);
}