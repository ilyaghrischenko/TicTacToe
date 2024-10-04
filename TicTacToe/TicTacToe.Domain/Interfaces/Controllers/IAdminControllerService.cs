using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.Controllers;

public interface IAdminControllerService
{
    Task<List<User>?> GetAppealedUsers();
    Task<List<Report>> GetUserReports(int userId);
    Task BlockUser(int userId);
}