using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.Services;

public interface IRoleService
{
    public Task<List<Role>?> GetRoles();
    public Task<Role?> GetRole(int id);
    public Task<bool> AddRole(Role role);
    public Task<bool> RemoveRole(int id);
    public Task<bool> UpdateRole(Role role);
}