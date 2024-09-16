using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.ModelsServices;

namespace Mafia.Application.Services.Models;

public class GameService : IGameService
{
    public async Task<bool> Start(List<User> players)
    {
        if (players.Count < 5) return false;

        players.ForEach(player =>
        {
            //TODO: end this class
        });
        
        return true;
    }
}