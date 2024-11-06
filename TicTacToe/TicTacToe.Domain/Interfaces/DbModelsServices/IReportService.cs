using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.DbModelsServices;

public interface IReportService
{
    Task<List<Report>> GetUserReportsAsync(int userId);
    Task SendReportAsync(int id, string message);
}