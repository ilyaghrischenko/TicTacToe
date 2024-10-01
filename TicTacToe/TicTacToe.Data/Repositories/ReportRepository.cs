using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Data.Repositories;

public class ReportRepository(TicTacToeContext context) : IRepository<Report>
{
    private readonly TicTacToeContext _context = context;
    
    public async Task<List<Report>?> GetAll()
    {
        return await _context.Reports
            .Include(u => u.User)
            .ToListAsync();
    }

    public async Task<Report?> Get(int id)
    {
        return await _context.Reports
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> Add(Report entity)
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

    public async Task<bool> Update(Report entity, Action updateAction)
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

    public async Task<bool> Delete(int id)
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