using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Service;

public class FriendService(IFriendRepository repository) : IFriendService
{
    private readonly IFriendRepository _repository = repository;
    
    public async Task<List<Friend>?> GetFriends()
    {
        return await _repository.GetFriends() ?? null;
    }

    public async Task<Friend?> GetFriend(int id)
    {
        return await _repository.GetFriend(id) ?? null;
    }

    public async Task<bool> AddFriend(Friend friend)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveFriend(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateFriend(Friend friend)
    {
        throw new NotImplementedException();
    }
}