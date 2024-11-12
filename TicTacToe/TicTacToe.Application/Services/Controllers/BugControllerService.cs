using Microsoft.AspNetCore.Mvc;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Models;

namespace TicTacToe.Application.Services.Controllers;

public class BugControllerService(IBugService bugService) : IBugControllerService
{
    private readonly IBugService _bugService = bugService;
    
    public async Task SendBugAsync(BugModel bug)
    {
        var newBug = new Bug(bug.Action, bug.Description, bug.Importance);
        await _bugService.SendBugAsync(newBug);
    }

    public async Task<List<Bug>> GetAllBugsAsync()
    {
        return await _bugService.GetAllBugsAsync();
    }
    
    public async Task<List<Bug>> GetBugsByStatusAsync(BugStatus status)
    {
        return await _bugService.GetBugsByStatusAsync(status);
    }
}