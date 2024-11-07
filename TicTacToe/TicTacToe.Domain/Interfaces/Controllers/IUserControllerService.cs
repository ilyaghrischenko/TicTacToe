using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.Controllers;

public interface IUserControllerService : IBaseControllerService, ICurrentUserId
{
    public Task<User> GetUserAsync();
    public Task<Statistic> GetUserStatisticsAsync();
    public Task<List<User>?> GetFriendsAsync();
    public Task<List<User>?> GetAllUsersAsync();
}