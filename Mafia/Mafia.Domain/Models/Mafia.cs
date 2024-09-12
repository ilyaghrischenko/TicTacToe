using Mafia.Domain.Enums;
using Mafia.Domain.Interfaces.Repositories;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Models;

namespace Mafia.Domain.Models;

public class Mafia(IMafiaService mafiaService): Role
{
    private readonly IMafiaService _mafiaService = mafiaService;
    
    public async Task<bool> Kill(int userId)
    {
        return await _mafiaService.Kill(userId);
    }
}