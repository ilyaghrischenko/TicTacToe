using Domain.DbModels;
using Domain.Enums;
using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;

namespace Domain.Models;

public class Slut(ISlutService slutService): Role
{
    private readonly ISlutService _slutService = slutService;

    public async Task<bool> Blocked(int userId)
    {
        return await _slutService.Blocked(userId);
    }
}