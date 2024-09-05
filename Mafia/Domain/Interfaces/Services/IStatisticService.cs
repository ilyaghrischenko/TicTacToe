using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IStatisticService
{
    public Task<List<Statistic>?> GetStatistics();
    public Task<Statistic?> GetStatistic(int id);
    public Task<bool> AddStatistic(Statistic statistic);
    public Task<bool> RemoveStatistic(int id);
    public Task<bool> UpdateStatistic(Statistic statistic);
}