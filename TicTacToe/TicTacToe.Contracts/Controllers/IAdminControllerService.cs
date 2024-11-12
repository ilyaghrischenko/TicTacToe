using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Controllers;

public interface IAdminControllerService
{
    Task<List<User>?> GetAppealedUsersAsync();
    Task<List<Report>> GetUserReportsAsync(int userId);
    Task BlockUserAsync(int userId);
}