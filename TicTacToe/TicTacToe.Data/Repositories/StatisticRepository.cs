using TicTacToe.Domain.Interfaces.Repositories;
using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Data.Repositories;

public class StatisticRepository(TicTacToeContext context) : IRepository<Statistic>
{
    private readonly TicTacToeContext _context = context;
    
    public async Task<List<Statistic>?> GetAllAsync()
    {
        return await _context.Statistics.ToListAsync();
    }

    public async Task<Statistic?> GetAsync(int id)
    {
        return await _context.Statistics.FindAsync(id);
    }

    public async Task<bool> AddAsync(Statistic statistic)
    {
        try
        {
            await _context.Statistics.AddAsync(statistic);
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
            var statistic = await _context.Statistics.FindAsync(id);
            _context.Statistics.Remove(statistic);
            await _context.SaveChangesAsync();
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
            _context.Statistics.Update(statistic);
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