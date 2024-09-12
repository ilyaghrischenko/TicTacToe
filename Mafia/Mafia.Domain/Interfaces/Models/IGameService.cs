using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.Models;

public interface IGameService
{
    public Task<bool> Start(List<User> players);
}