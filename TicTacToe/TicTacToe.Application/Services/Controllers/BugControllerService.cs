using TicTacToe.Application.Services.DbModels;
using TicTacToe.Contracts.Interfaces.Controllers;
using TicTacToe.Domain.DbModels;
using TicTacToe.DTO.Models;

namespace TicTacToe.Application.Services.Controllers;

public class BugControllerService(BugService bugService) : IBugControllerService
{
    private readonly BugService _bugService = bugService;
    
    public async Task SendBugAsync(BugModel bug)
    {
        var newBug = new Bug(bug.Action, bug.Description, bug.Importance);
        await _bugService.SendBugAsync(newBug);
    }
}