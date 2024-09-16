using Mafia.Domain.Enums;
using Mafia.Domain.Interfaces.Repositories;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.ModelsServices;

namespace Mafia.Domain.Models;

public class Sheriff(ISheriffService sheriffService): GameRole
{
    private readonly ISheriffService _sheriffService = sheriffService;
    
    public async Task<bool> Check(int userId)
    {
        return await _sheriffService.Check(userId);
    }

    public async Task<bool> Shoot(int userId)
    {
        return await _sheriffService.Shoot(userId);
    }
}