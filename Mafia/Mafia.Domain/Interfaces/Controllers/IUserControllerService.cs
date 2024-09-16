using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.Controllers;

public interface IUserControllerService
{
    public Task<User> GetUser(int userId);
}