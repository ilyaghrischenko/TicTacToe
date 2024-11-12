using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.Controllers;

public interface IBugControllerService : IBaseControllerService
{
    Task SendBugAsync(BugModel bug);
    Task<List<object>> GetAllBugsAsync();
    Task<List<object>> GetBugsByStatusAsync(int status);
}