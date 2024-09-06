using Domain.DbModels;

namespace Domain.Interfaces.Services;

public interface IFriendService
{
    public Task<List<Friend>?> GetFriends();
    public Task<Friend?> GetFriend(int id);
    public Task<bool> AddFriend(Friend friend);
    public Task<bool> RemoveFriend(int id);
    public Task<bool> UpdateFriend(Friend friend);
}