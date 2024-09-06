using Domain.Interfaces.Repositories;
using Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<List<User>?> GetUsers()
    {
        using var context = new MafiaContext();
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUser(int id)
    {
        using var context = new MafiaContext();
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetUser(string login)
    {
        using var context = new MafiaContext();
        return await context.Users.FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<bool> AddUser(User user)
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

    public async Task<bool> RemoveUser(int id)
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

    public async Task<bool> UpdateUser(User user)
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