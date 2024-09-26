using TicTacToe.Domain.Interfaces.Repositories;
using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Data.Repositories;

public class RoleRepository(MafiaContext mafiaContext) : IRepository<GameRole>
{
    private readonly MafiaContext _context = mafiaContext;
    
    public async Task<List<GameRole>?> GetAll()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<GameRole?> Get(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<bool> Add(GameRole gameRole)
    {
        try
        {
            await _context.Roles.AddAsync(gameRole);
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
            var role = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update(GameRole gameRole, Action updateAction)
    {
        try
        {
            _context.Roles.Update(gameRole);
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