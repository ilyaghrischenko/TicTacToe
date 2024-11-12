using System.Threading.Tasks;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.DbModelsServices;

public interface IStatisticService
{
    public Task<Statistic> GetUserStatisticsAsync(int userId);
}