using Mafia.Domain.Enums;
using Mafia.Domain.Interfaces.Models;
using Mafia.Domain.Interfaces.Repositories;

namespace Mafia.Service.Services.Models;

public class SlutService(IUserRepository userRepository): ISlutService
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