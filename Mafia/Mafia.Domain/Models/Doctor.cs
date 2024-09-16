using Mafia.Domain.Enums;
using Mafia.Domain.Interfaces.Repositories;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.ModelsServices;

namespace Mafia.Domain.Models;

public class Doctor(IDoctorService doctorService): GameRole
{
    private readonly IDoctorService _doctorService = doctorService;
    
    public async Task<bool> Cure(int userId)
    {
        return await _doctorService.Cure(Id, userId);
    }
}