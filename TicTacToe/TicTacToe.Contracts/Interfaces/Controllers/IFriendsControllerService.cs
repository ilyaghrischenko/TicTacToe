namespace TicTacToe.Contracts.Interfaces.Controllers;

public interface IFriendsControllerService: IBaseControllerService
{
    public Task AddFriendAsync(int newFriendId);
    public Task DeleteFriendAsync(int friendId);
}