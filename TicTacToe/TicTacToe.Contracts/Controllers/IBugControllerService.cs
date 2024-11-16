using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.Controllers;

public interface IBugControllerService : IBaseControllerService
{
    Task SendBugAsync(SendBugRequest sendBug);
    Task ChangeBugStatusAsync(BugChangeStatusRequest request);
    Task<List<BugResponse>> GetAllBugsAsync();
    Task<List<BugResponse>> GetBugsByStatusAsync(int status);
}