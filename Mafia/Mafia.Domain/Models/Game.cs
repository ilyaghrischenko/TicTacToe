using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.ModelsServices;

namespace Mafia.Domain.Models;

public class Game(List<User> players, IGameService gameService)
{
    public List<User> Players { get; set; } = players;
    private readonly IGameService _gameService = gameService;

    public async Task<bool> Start()
    {
        return await _gameService.Start(Players);
    }
}