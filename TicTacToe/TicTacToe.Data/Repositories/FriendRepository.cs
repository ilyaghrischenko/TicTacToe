using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Data.Repositories;

public class FriendRepository(TicTacToeContext context) : IFriendRepository
{
    private readonly TicTacToeContext _context = context;
    
    public async Task<List<Friend>?> GetAll()
    {
        return await _context.Friends.ToListAsync();
    }

    public async Task<Friend?> Get(int id)
    {
        return await _context.Friends.FindAsync(id);
    }

    public async Task<Friend?> Get(int userId, int friendId)
    {
        return await _context.Friends.FirstAsync(x => x.UserId == userId && x.FriendUserId == friendId);
    }

    public async Task<List<User>?> GetUserFriends(int userId)
    {
        var friends = await _context.Friends
            .Include(friend => friend.FriendUser).ToListAsync();
        var userFriends = new List<User>();
        friends.ForEach(friend =>
            userFriends.Add(friend.FriendUser));
        return userFriends;
    }

    public async Task<bool> Add(Friend entity)
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

    public async Task<bool> Update(Friend entity, Action updateAction)
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

    public async Task<bool> Delete(int id)
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