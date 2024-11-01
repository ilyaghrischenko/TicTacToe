using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Domain.Interfaces;

public interface IGameService
{
    Task Win(int id);
    Task Lose(int id);
}