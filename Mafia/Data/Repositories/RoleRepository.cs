using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RoleRepository : IRoleRepository
{
    public async Task<List<Role>?> GetRoles()
    {
        using var context = new MafiaContext();
        return await context.Roles.ToListAsync();
    }

    public async Task<Role?> GetRole(int id)
    {
        using var context = new MafiaContext();
        return await context.Roles.FindAsync(id);
    }

    public async Task<bool> AddRole(Role role)
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

    public async Task<bool> RemoveRole(int id)
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

    public async Task<bool> UpdateRole(Role role)
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