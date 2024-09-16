using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.Repositories;

namespace Mafia.Application.Services.Controllers;

public class UserControllerService(IUserRepository userRepository) : IUserControllerService
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<User> GetUser(int userId)
    {
        var user = await _userRepository.Get(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        
        return user;
    }
}