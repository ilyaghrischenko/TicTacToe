using Domain.DbModels;
using Domain.Enums;
using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;

namespace Domain.Models;

public class Doctor(IDoctorService doctorService): Role
{
    private readonly IDoctorService _doctorService = doctorService;
    
    public async Task<bool> Cure(int userId)
    {
        return await _doctorService.Cure(Id, userId);
    }
}