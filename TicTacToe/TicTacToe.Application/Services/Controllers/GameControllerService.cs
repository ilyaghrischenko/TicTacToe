using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Application.Services.Controllers;

public class GameControllerService(IGameService gameService) : IGameControllerService
{
    private readonly IGameService _gameService = gameService;

    public async Task<string> Win()
    {
        return await _gameService.Win();
    }

    public async Task<string> Lose()
    {
        return await _gameService.Lose();
    }
}