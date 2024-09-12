using Domain.DbModels;
using Domain.Interfaces.Models;

namespace Domain.Models;

public class Game(List<User> players, IGameService gameService)
{
    public List<User> Players { get; set; } = players;
    private readonly IGameService _gameService = gameService;

    public async Task<bool> Start()
    {
        return await _gameService.Start(Players);
    }
}