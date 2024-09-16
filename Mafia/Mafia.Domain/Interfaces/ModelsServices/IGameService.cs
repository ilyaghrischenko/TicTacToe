using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.ModelsServices;

public interface IGameService
{
    public Task<bool> Start(List<User> players);
}