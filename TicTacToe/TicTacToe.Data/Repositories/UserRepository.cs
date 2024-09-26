using TicTacToe.Domain.Interfaces.Repositories;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TicTacToe.Data.Repositories;

public class UserRepository(MafiaContext context) : IUserRepository
{
    private readonly MafiaContext _context = context;
    
    public async Task<List<User>?> GetAll()
    {
        return await _context.Users
            .Include(u => u.Statistic)
            .ToListAsync();
    }

    public async Task<User?> Get(int id)
    {
        return await _context.Users
            .Include(u => u.Statistic)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> Get(string login)
    {
        return await _context.Users
            .Include(u => u.Statistic)
            .FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<bool> Add(User user)
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

    public async Task<bool> Delete(int id)
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

    public async Task<bool> Update(User user, Action updateAction)
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