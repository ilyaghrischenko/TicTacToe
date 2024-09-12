using Domain.Enums;
using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;

namespace Service.Services.Models;

public class DoctorService(IUserRepository userRepository): IDoctorService
{
    private readonly IUserRepository _userRepository = userRepository;
    private bool _canCureSelf = true;
    
    public async Task<bool> Cure(int selfId, int userId)
    {
        if (!_canCureSelf) return false;
        
        var userToCure = await _userRepository.Get(userId);
        if (userToCure == null) return false;

        if (selfId == userId) _canCureSelf = false;
        
        userToCure.Role.Statuses.Add(Status.Healed);
        await _userRepository.Update(userToCure);
        return true;
    }
}