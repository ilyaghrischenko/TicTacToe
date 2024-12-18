using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Controllers;

public interface IUserControllerService : IBaseControllerService, ICurrentUserId
{
    public Task<User> GetUserAsync();
    public Task<Statistic> GetUserStatisticsAsync();
    public Task<List<User>?> GetFriendsAsync();
    public Task<List<User>?> GetAllUsersAsync();
}