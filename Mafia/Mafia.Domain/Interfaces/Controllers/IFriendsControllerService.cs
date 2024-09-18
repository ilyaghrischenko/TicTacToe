namespace Mafia.Domain.Interfaces.Controllers;

public interface IFriendsControllerService
{
    public Task<bool> AddFriend(int newFriendId);
    public Task<bool> DeleteFriend(int friendId);
}