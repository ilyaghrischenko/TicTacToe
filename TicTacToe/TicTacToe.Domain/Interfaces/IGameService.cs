using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Domain.Interfaces;

public interface IGameService
{
    Task Win(string login);
    Task Lose(string login);
}