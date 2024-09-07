using Domain.Interfaces.Repositories;
using Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class FriendRepository : IRepository<Friend>
{
    public async Task<List<Friend>?> GetAll()
    {
        using var context = new MafiaContext();
        return await context.Friends.ToListAsync();
    }

    public async Task<Friend?> Get(int id)
    {
        using var context = new MafiaContext();
        return await context.Friends.FindAsync(id);
    }

    public async Task<bool> Add(Friend friend)
    {
        using var context = new MafiaContext();
        try
        {
            await context.Friends.AddAsync(friend);
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
            var friend = await context.Friends.FindAsync(id);
            context.Friends.Remove(friend);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update(Friend friend)
    {
        using var context = new MafiaContext();
        try
        {
            context.Friends.Update(friend);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}