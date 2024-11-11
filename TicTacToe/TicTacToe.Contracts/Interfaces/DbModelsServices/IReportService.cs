using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Interfaces.DbModelsServices;

public interface IReportService
{
    Task<List<Report>> GetUserReportsAsync(int userId);
    Task SendReportAsync(int id, string message);
    Task DeleteAllUserReportsAsync(int userId);
}