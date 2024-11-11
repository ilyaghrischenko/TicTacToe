using Microsoft.EntityFrameworkCore;
using TicTacToe.Contracts.Interfaces.Repositories;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Data.Repositories;

public class ReportRepository(TicTacToeContext context) : IRepository<Report>
{
    private readonly TicTacToeContext _context = context;
    
    public async Task<List<Report>?> GetAllAsync()
    {
        return await _context.Reports
            .Include(u => u.User)
            .Include(u => u.User.Statistic)
            .ToListAsync();
    }

    public async Task<Report?> GetAsync(int id)
    {
        return await _context.Reports
            .Include(u => u.User)
            .Include(u => u.User.Statistic)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> AddAsync(Report entity)
    {
        try
        {
            await _context.Reports.AddAsync(entity);
            await _context.SaveChangesAsync();
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
            _context.Reports.Update(entity);
            updateAction();
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
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}