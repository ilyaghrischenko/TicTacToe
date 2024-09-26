using Mafia.Domain.Enums;
using Mafia.Domain.Interfaces.ModelsServices;
using Mafia.Domain.Interfaces.Repositories;

namespace Mafia.Application.Services.Models;

public class SheriffService(IUserRepository userRepository): ISheriffService
{
    private readonly IUserRepository _userRepository = userRepository;
    private bool _canShoot = true;
    
    public async Task<bool> Check(int userId)
    {
        var userToCheck = await _userRepository.Get(userId);
        if (userToCheck == null) return false;

        return userToCheck.GameRole.RoleName == RoleName.Mafia;
    }

    public async Task<bool> Shoot(int userId)
    {
        if (!_canShoot) return false;
        
        var userToShoot = await _userRepository.Get(userId);
        if (userToShoot == null) return false;
        
        await _userRepository.Update(userToShoot, () =>
        {
            userToShoot.GameRole.Statuses.Add(Status.Shot);
        });
        
        _canShoot = false;
        return true;
    }
}