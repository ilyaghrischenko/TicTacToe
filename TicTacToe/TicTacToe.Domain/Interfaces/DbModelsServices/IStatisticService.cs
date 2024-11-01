using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.DbModelsServices;

public interface IStatisticService
{
    public Task<Statistic> GetUserStatistics(int userId);
}