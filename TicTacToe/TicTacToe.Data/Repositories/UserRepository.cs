using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TicTacToe.Contracts.Interfaces.Repositories;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Data.Repositories;

public class UserRepository(TicTacToeContext context) : IUserRepository
{
    private readonly TicTacToeContext _context = context;

    public async Task<List<User>?> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Statistic)
            .ToListAsync();
    }

    public async Task<User?> GetAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Statistic)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetAsync(string login)
    {
        return await _context.Users
            .Include(u => u.Statistic)
            .FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<User?> GetAdminAsync()
    {
        return await _context.Users
            .Include(u => u.Statistic)
            .FirstOrDefaultAsync(user => user.Role == Role.Admin);
    }

    public async Task<bool> AddAsync(User user)
    {
        var statistic = new Statistic();
        await _context.Statistics.AddAsync(statistic);
        await _context.SaveChangesAsync();
        user.Statistic = await _context.Statistics.FirstAsync(s => s.Id == statistic.Id);

        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
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
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
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
            _context.Users.Update(user);
            updateAction();
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}