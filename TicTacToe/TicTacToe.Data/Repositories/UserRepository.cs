using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Data.Repositories;

public class UserRepository(IDbContextFactory<TicTacToeContext> contextFactory) : IUserRepository
{
    private readonly IDbContextFactory<TicTacToeContext> _contextFactory = contextFactory;

    public async Task<List<User>?> GetAllAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        
        return await context.Users
            .Include(u => u.Statistic)
            .ToListAsync();
    }

    public async Task<User?> GetAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.Users
            .Include(u => u.Statistic)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetAsync(string login)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.Users
            .Include(u => u.Statistic)
            .FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<User?> GetAdminAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.Users
            .Include(u => u.Statistic)
            .FirstOrDefaultAsync(user => user.Role == Role.Admin);
    }

    public async Task<bool> AddAsync(User user)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var statistic = new Statistic();
        await context.Statistics.AddAsync(statistic);
        await context.SaveChangesAsync();
        user.Statistic = await context.Statistics.FirstAsync(s => s.Id == statistic.Id);

        try
        {
            await context.Users.AddAsync(user);
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

            var user = await context.Users.FindAsync(id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(User user, Action updateAction)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            context.Users.Update(user);
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