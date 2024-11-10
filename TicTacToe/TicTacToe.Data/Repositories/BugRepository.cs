using Microsoft.EntityFrameworkCore;
using TicTacToe.Contracts.Interfaces.Repositories;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Data.Repositories;

public class BugRepository(TicTacToeContext context) : IRepository<Bug>
{
    private readonly TicTacToeContext _context = context;
    
    public async Task<List<Bug>?> GetAllAsync()
    {
        return await _context.Bugs.ToListAsync();
    }

    public async Task<Bug?> GetAsync(int id)
    {
        return await _context.Bugs.FindAsync(id);
    }

    public async Task<bool> AddAsync(Bug entity)
    {
        try
        {
            await _context.Bugs.AddAsync(entity);
            await _context.SaveChangesAsync();
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
            _context.Bugs.Update(entity);
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
            var bug = await _context.Bugs.FindAsync(id);
            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}