using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Contracts.Repositories;

namespace TicTacToe.Data.Repositories;

public class FriendRepository(TicTacToeContext context) : IFriendRepository
{
    private readonly TicTacToeContext _context = context;
    
    public async Task<List<Friend>?> GetAllAsync()
    {
        return await _context.Friends.ToListAsync();
    }

    public async Task<Friend?> GetAsync(int id)
    {
        return await _context.Friends.FindAsync(id);
    }

    public async Task<Friend?> GetAsync(int userId, int friendId)
    {
        return await _context.Friends.FirstAsync(x => x.UserId == userId && x.FriendUserId == friendId);
    }

    public async Task<List<User>?> GetUserFriendsAsync(int userId)
    {
        var friends = await _context.Friends
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
            await _context.Friends.AddAsync(entity);
            await _context.SaveChangesAsync();
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
            _context.Friends.Update(entity);
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
            var friend = await context.Friends.FindAsync(id);
            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}