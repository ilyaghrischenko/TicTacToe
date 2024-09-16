using Mafia.Domain.Enums;
using Mafia.Domain.Interfaces.Repositories;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.ModelsServices;

namespace Mafia.Domain.Models;

public class Slut(ISlutService slutService): GameRole
{
    private readonly ISlutService _slutService = slutService;

    public async Task<bool> Blocked(int userId)
    {
        return await _slutService.Blocked(userId);
    }
}