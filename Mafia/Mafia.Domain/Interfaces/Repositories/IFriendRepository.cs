using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.Repositories;

public interface IFriendRepository : IRepository<Friend>
{
    public Task<List<User>?> GetUserFriends(int userId);
    public Task<Friend?> Get(int userId, int friendId);
}