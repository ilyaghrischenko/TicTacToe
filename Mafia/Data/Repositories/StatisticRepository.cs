using Domain.Interfaces.Repositories;
using Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class StatisticRepository : IStatisticRepository
{
    public async Task<List<Statistic>?> GetStatistics()
    {
        using var context = new MafiaContext();
        return await context.Statistics.ToListAsync();
    }

    public async Task<Statistic?> GetStatistic(int id)
    {
        using var context = new MafiaContext();
        return await context.Statistics.FindAsync(id);
    }

    public async Task<bool> AddStatistic(Statistic statistic)
    {
        using var context = new MafiaContext();
        try
        {
            await context.Statistics.AddAsync(statistic);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveStatistic(int id)
    {
        using var context = new MafiaContext();
        try
        {
            var statistic = await context.Statistics.FindAsync(id);
            context.Statistics.Remove(statistic);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateStatistic(Statistic statistic)
    {
        using var context = new MafiaContext();
        try
        {
            context.Statistics.Update(statistic);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}