using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Contracts.Repositories;

namespace TicTacToe.Data.Repositories;

public class FriendRepository(IDbContextFactory<TicTacToeContext> contextFactory) : IFriendRepository
{
    private readonly IDbContextFactory<TicTacToeContext> _contextFactory = contextFactory;
    
    public async Task<List<Friend>?> GetAllAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var friends = await context.Friends.ToListAsync();
        return friends;
    }

    public async Task<Friend?> GetAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Friends.FindAsync(id);
    }

    public async Task<Friend?> GetAsync(int userId, int friendId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Friends
            .FirstAsync(x =>
                x.UserId == userId
                && x.FriendUserId == friendId);
    }

    public async Task<List<User>?> GetUserFriendsAsync(int userId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        
        var friends = await context.Friends
            .Include(friend => friend.FriendUser)
            .Include(friend => friend.FriendUser.Statistic)
            .Where(x => x.UserId == userId)
            .ToListAsync();
        var userFriends = new List<User>();
        friends.ForEach(friend =>
            userFriends.Add(friend.FriendUser));
        return userFriends;
    }

    public async Task<bool> AddAsync(Friend entity)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            await context.Friends.AddAsync(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Friend entity, Action updateAction)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Friends.Update(entity);
            updateAction();
            await context.SaveChangesAsync();
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
            using var context = await _contextFactory.CreateDbContextAsync();
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
}