using Domain.Interfaces.Repositories;
using Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class StatisticRepository : IRepository<Statistic>
{
    public async Task<List<Statistic>?> GetAll()
    {
        using var context = new MafiaContext();
        return await context.Statistics.ToListAsync();
    }

    public async Task<Statistic?> Get(int id)
    {
        using var context = new MafiaContext();
        return await context.Statistics.FindAsync(id);
    }

    public async Task<bool> Add(Statistic statistic)
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

    public async Task<bool> Delete(int id)
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

    public async Task<bool> Update(Statistic statistic)
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