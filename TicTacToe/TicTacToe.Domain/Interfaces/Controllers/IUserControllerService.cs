using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.Controllers;

public interface IUserControllerService : IBaseControllerService
{
    public Task<User> GetUser();
    public Task<List<User>?> GetFriends();
    public Task<List<User>?> GetAllUsers();
    public int GetCurrentUserId();
}