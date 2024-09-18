using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.Controllers;

public interface IUserControllerService
{
    public Task<User> GetUser();
    public Task<List<Friend>?> GetFriends();
    public Task<List<User>?> GetAllUsers();
}