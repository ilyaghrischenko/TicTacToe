using Domain.DbModels;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Domain.Models;

public class Mafia(IUserRepository userRepository): Role
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<bool> Kill(int userId)
    {
        var userToKill = await _userRepository.Get(userId);
        if (userToKill == null) return false;

        userToKill.Role.Statuses.Add(Status.Killed);
        await _userRepository.Update(userToKill);
        return true;
    }
}