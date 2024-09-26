using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.Controllers;

public interface IUserControllerService : IBaseControllerService
{
    public Task<User> GetUser();
    public Task<List<User>?> GetFriends();
    public Task<List<User>?> GetAllUsers();
}