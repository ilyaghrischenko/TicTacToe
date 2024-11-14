using Microsoft.EntityFrameworkCore;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Data.Repositories;

public class ReportRepository(IDbContextFactory<TicTacToeContext> contextFactory) : IRepository<Report>
{
    private readonly IDbContextFactory<TicTacToeContext> _contextFactory = contextFactory;
    
    public async Task<List<Report>?> GetAllAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        
        return await context.Reports
            .Include(u => u.User)
            .Include(u => u.User.Statistic)
            .ToListAsync();
    }

    public async Task<Report?> GetAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.Reports
            .Include(u => u.User)
            .Include(u => u.User.Statistic)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> AddAsync(Report entity)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            await context.Reports.AddAsync(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Report entity, Action updateAction)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Reports.Update(entity);
            updateAction();
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
            var report = await context.Reports.FindAsync(id);
            context.Reports.Remove(report);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}