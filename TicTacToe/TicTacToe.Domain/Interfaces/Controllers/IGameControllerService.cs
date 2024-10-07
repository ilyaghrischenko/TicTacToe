namespace TicTacToe.Domain.Interfaces.Controllers;

public interface IGameControllerService
{
    Task<string> Win();
    Task<string> Lose();
}