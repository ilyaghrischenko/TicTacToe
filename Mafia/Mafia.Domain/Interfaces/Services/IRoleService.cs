using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.Services;

public interface IRoleService
{
    public Task<List<GameRole>?> GetRoles();
    public Task<GameRole?> GetRole(int id);
    public Task<bool> AddRole(GameRole gameRole);
    public Task<bool> RemoveRole(int id);
    public Task<bool> UpdateRole(GameRole gameRole);
}