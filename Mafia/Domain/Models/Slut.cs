using Domain.DbModels;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Domain.Models;

public class Slut(IUserRepository userRepository): Role
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<bool> Blocked(int userId)
    {
        var userToBlock = await _userRepository.Get(userId);
        if (userToBlock == null) return false;
        
        userToBlock.Role.Statuses.Add(Status.Silenced);
        await _userRepository.Update(userToBlock);
        return true;
    }
}