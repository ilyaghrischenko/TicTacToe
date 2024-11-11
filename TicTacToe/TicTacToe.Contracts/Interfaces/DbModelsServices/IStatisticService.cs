using System.Threading.Tasks;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Interfaces.DbModelsServices;

public interface IStatisticService
{
    public Task<Statistic> GetUserStatisticsAsync(int userId);
}