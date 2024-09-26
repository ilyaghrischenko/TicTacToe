using TicTacToe.Domain.Interfaces.Repositories;
using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Data.Repositories;

public class StatisticRepository(MafiaContext context) : IRepository<Statistic>
{
    private readonly MafiaContext _context = context;
    
    public async Task<List<Statistic>?> GetAll()
    {
        return await _context.Statistics.ToListAsync();
    }

    public async Task<Statistic?> Get(int id)
    {
        return await _context.Statistics.FindAsync(id);
    }

    public async Task<bool> Add(Statistic statistic)
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

    public async Task<bool> Delete(int id)
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

    public async Task<bool> Update(Statistic statistic, Action updateAction)
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