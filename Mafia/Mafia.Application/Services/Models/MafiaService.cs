using Mafia.Domain.Enums;
using Mafia.Domain.Interfaces.ModelsServices;
using Mafia.Domain.Interfaces.Repositories;

namespace Mafia.Application.Services.Models;

public class MafiaService(IUserRepository userRepository): IMafiaService
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<bool> Kill(int userId)
    {
        var userToKill = await _userRepository.Get(userId);
        if (userToKill == null) return false;

        userToKill.GameRole.Statuses.Add(Status.Killed);
        await _userRepository.Update(userToKill);
        return true;
    }
}