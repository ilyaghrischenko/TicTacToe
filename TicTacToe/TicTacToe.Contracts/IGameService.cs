namespace TicTacToe.Contracts;

public interface IGameService
{
    Task WinAsync(int id);
    Task LoseAsync(int id);
}