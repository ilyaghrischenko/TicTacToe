using Domain.DbModels;

namespace Domain.Interfaces.Models;

public interface IGameService
{
    public Task<bool> Start(List<User> players);
}