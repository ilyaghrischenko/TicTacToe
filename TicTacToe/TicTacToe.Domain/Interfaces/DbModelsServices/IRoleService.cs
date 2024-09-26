using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.DbModelsServices;

public interface IRoleService
{
    public Task<List<GameRole>?> GetRoles();
    public Task<GameRole?> GetRole(int id);
    public Task<bool> AddRole(GameRole gameRole);
    public Task<bool> RemoveRole(int id);
    public Task<bool> UpdateRole(GameRole gameRole);
}