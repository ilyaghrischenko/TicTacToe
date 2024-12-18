using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Contracts.Repositories;

namespace TicTacToe.Data.Repositories;

public class StatisticRepository(IDbContextFactory<TicTacToeContext> contextFactory) : IRepository<Statistic>
{
    private readonly IDbContextFactory<TicTacToeContext> _contextFactory = contextFactory;
    
    public async Task<List<Statistic>?> GetAllAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Statistics.ToListAsync();
    }

    public async Task<Statistic?> GetAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Statistics.FindAsync(id);
    }

    public async Task<bool> AddAsync(Statistic statistic)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            await context.Statistics.AddAsync(statistic);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            
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

    public async Task<bool> UpdateAsync(Statistic statistic, Action updateAction)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            context.Statistics.Update(statistic);
            updateAction();
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}