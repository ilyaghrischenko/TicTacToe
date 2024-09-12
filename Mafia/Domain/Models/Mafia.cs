using Domain.DbModels;
using Domain.Enums;
using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;

namespace Domain.Models;

public class Mafia(IMafiaService mafiaService): Role
{
    private readonly IMafiaService _mafiaService = mafiaService;
    
    public async Task<bool> Kill(int userId)
    {
        return await _mafiaService.Kill(userId);
    }
}