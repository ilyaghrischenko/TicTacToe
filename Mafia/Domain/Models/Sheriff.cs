using Domain.DbModels;
using Domain.Enums;
using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;

namespace Domain.Models;

public class Sheriff(ISheriffService sheriffService): Role
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