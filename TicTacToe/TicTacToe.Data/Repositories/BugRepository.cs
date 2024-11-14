using Microsoft.EntityFrameworkCore;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Data.Repositories;

public class BugRepository(IDbContextFactory<TicTacToeContext> contextFactory) : IRepository<Bug>
{
    private readonly IDbContextFactory<TicTacToeContext> _contextFactory = contextFactory;
    
    public async Task<List<Bug>?> GetAllAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var allBugs = await context.Bugs.ToListAsync();
        return allBugs;
    }

    public async Task<Bug?> GetAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var bug = await context.Bugs.FindAsync(id);
        return bug;
    }

    public async Task<bool> AddAsync(Bug entity)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            await context.Bugs.AddAsync(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Bug entity, Action updateAction)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Bugs.Update(entity);
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
            var bug = await context.Bugs.FindAsync(id);
            context.Bugs.Remove(bug);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}