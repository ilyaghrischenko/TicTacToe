using Domain.Enums;
using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;

namespace Service.Services.Models;

public class MafiaService(IUserRepository userRepository): IMafiaService
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