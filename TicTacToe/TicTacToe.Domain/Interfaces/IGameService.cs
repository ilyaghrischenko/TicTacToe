using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Domain.Interfaces;

public interface IGameService : ICurrentUserId
{
    Task<string> Win();
    Task<string> Lose();
}