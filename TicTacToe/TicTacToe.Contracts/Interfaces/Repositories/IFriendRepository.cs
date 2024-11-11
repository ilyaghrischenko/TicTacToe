using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Interfaces.Repositories;

public interface IFriendRepository : IRepository<Friend>
{
    public Task<List<User>?> GetUserFriendsAsync(int userId);
    public Task<Friend?> GetAsync(int userId, int friendId);
}