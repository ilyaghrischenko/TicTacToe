namespace TicTacToe.Contracts.Interfaces;

public interface IGameService
{
    Task WinAsync(int id);
    Task LoseAsync(int id);
}