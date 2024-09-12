using Mafia.Domain.Interfaces.Repositories;
using Mafia.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Mafia.Data.Repositories;

public class RoleRepository : IRepository<Role>
{
    public async Task<List<Role>?> GetAll()
    {
        using var context = new MafiaContext();
        return await context.Roles.ToListAsync();
    }

    public async Task<Role?> Get(int id)
    {
        using var context = new MafiaContext();
        return await context.Roles.FindAsync(id);
    }

    public async Task<bool> Add(Role role)
    {
        using var context = new MafiaContext();
        try
        {
            await context.Roles.AddAsync(role);
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

    public async Task<bool> Update(Role role)
    {
        using var context = new MafiaContext();
        try
        {
            context.Roles.Update(role);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}