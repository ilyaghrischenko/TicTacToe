using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Interfaces.Controllers;

public interface IUserControllerService : IBaseControllerService, ICurrentUserId
{
    public Task<User> GetUserAsync();
    public Task<Statistic> GetUserStatisticsAsync();
    public Task<List<User>?> GetFriendsAsync();
    public Task<List<User>?> GetAllUsersAsync();
}