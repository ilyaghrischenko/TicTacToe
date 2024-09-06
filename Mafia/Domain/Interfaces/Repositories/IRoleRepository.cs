using Domain.DbModels;

namespace Domain.Interfaces.Repositories;

public interface IRoleRepository
{
    public Task<List<Role>?> GetRoles();
    public Task<Role?> GetRole(int id);
    public Task<bool> AddRole(Role role);
    public Task<bool> RemoveRole(int id);
    public Task<bool> UpdateRole(Role role);
}