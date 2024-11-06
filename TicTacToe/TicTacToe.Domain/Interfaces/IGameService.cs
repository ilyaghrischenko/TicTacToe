using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Domain.Interfaces;

public interface IGameService
{
    Task WinAsync(int id);
    Task LoseAsync(int id);
}