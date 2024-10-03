using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.DbModelsServices;

public interface IReportService
{
    Task<List<Report>> GetUserReports(int userId);
}