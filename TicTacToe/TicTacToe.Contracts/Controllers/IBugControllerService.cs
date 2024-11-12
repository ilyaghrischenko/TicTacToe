using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.Controllers;

public interface IBugControllerService : IBaseControllerService
{
    Task SendBugAsync(BugModel bug);
}