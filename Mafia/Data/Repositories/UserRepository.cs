using Domain.Interfaces.Repositories;
using Domain.DbModels;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<List<User>?> GetAll()
    {
        using var context = new MafiaContext();
        return await context.Users.ToListAsync();
    }

    public async Task<User?> Get(int id)
    {
        using var context = new MafiaContext();
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> Get(string login)
    {
        using var context = new MafiaContext();
        return await context.Users.FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<bool> Add(User user)
    {
        using var context = new MafiaContext();
        try
        {
            await context.Users.AddAsync(user);
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
            var user = await context.Users.FindAsync(id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update(User user)
    {
        using var context = new MafiaContext();
        try
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}