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
    
    public async Task SendBugAsync(SendBugRequest sendBug)
    {
        var newBug = new Bug(sendBug.Action, sendBug.Description, sendBug.Importance);
        await _bugService.SendBugAsync(newBug);
    }

    public async Task ChangeBugStatusAsync(BugChangeStatusRequest request)
    {
        var bugStatus = (BugStatus)request.Status;
        await _bugService.ChangeBugStatusAsync(request.Id, bugStatus);
    }

    public async Task<List<BugResponse>> GetAllBugsAsync()
    {
        var bugs = await _bugService.GetAllBugsAsync();
        
        var bugsResponse = new List<BugResponse>();
        foreach (var bug in bugs)
        {
            var item = new BugResponse(bug.Id,
                bug.Description,
                bug.Action.ToString(),
                bug.Importance.ToString(),
                (int)bug.Status,
                bug.CreatedAt);
            
            bugsResponse.Add(item);
        }
        return bugsResponse;
    }
    
    public async Task<List<BugResponse>> GetBugsByStatusAsync(int status)
    {
        var bugs = await _bugService.GetBugsByStatusAsync(status);
        
        var bugsResponse = new List<BugResponse>();
        foreach (var bug in bugs)
        {
            var item = new BugResponse(bug.Id,
                bug.Description,
                bug.Action.ToString(),
                bug.Importance.ToString(),
                (int)bug.Status,
                bug.CreatedAt);
            
            bugsResponse.Add(item);
        }
        return bugsResponse;
    }
}