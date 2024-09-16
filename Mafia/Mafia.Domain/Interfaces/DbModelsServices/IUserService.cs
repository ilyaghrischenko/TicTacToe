using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.DbModelsServices;

public interface IUserService
{
    public Task<List<User>?> GetUsers();
    public Task<User?> GetUser(int id);
    public Task<bool> AddUser(User user);
    public Task<bool> RemoveUser(int id);
    public Task<bool> UpdateUser(User user);
}