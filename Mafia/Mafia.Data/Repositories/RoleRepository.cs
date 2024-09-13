using Mafia.Domain.Interfaces.Repositories;
using Mafia.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Mafia.Data.Repositories;

public class RoleRepository : IRepository<GameRole>
{
    public async Task<List<GameRole>?> GetAll()
    {
        using var context = new MafiaContext();
        return await context.Roles.ToListAsync();
    }

    public async Task<GameRole?> Get(int id)
    {
        using var context = new MafiaContext();
        return await context.Roles.FindAsync(id);
    }

    public async Task<bool> Add(GameRole gameRole)
    {
        using var context = new MafiaContext();
        try
        {
            await context.Roles.AddAsync(gameRole);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        using var context = new MafiaContext();
        try
        {
            var role = await context.Roles.FindAsync(id);
            context.Roles.Remove(role);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update(GameRole gameRole)
    {
        using var context = new MafiaContext();
        try
        {
            context.Roles.Update(gameRole);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}