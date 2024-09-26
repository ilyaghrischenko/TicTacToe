namespace TicTacToe.Domain.Interfaces.Controllers;

public interface IFriendsControllerService: IBaseControllerService
{
    public Task AddFriend(int newFriendId);
    public Task DeleteFriend(int friendId);
}