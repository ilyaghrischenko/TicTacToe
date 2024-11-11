using System.Threading.Tasks;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.Interfaces.Controllers;

public interface IBugControllerService
{
    Task SendBugAsync(BugModel bug);
}