using Domain.DbModels;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Domain.Models;

public class Doctor(IUserRepository userRepository): Role
{
    private readonly IUserRepository _userRepository = userRepository;
    private bool _canCureSelf = true;
    
    public async Task<bool> Cure(int userId)
    {
        if (!_canCureSelf) return false;
        
        var userToCure = await _userRepository.GetUser(userId);
        if (userToCure == null) return false;

        if (Id == userId) _canCureSelf = false;
        
        userToCure.Role.Statuses.Add(Status.Healed);
        await _userRepository.UpdateUser(userToCure);
        return true;
    }
}