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

    public async Task ChangeBugStatusAsync(BugChangeStatusRequest request)
    {
        var bugStatus = (BugStatus)request.Status;
        await _bugService.ChangeBugStatusAsync(request.Id, bugStatus);
    }

    public async Task<List<object>> GetAllBugsAsync()
    {
        var bugs = await _bugService.GetAllBugsAsync();
        
        var bugsDto = new List<object>();
        foreach (var bug in bugs)
        {
            var bugDto = new
            {
                id = bug.Id,
                description = bug.Description,
                action = bug.Action.ToString(),
                importance = bug.Importance.ToString(),
                status = (int)bug.Status,
                createdAt = bug.CreatedAt
            };
            bugsDto.Add(bugDto);
        }
        return bugsDto;
    }
    
    public async Task<List<object>> GetBugsByStatusAsync(int status)
    {
        var bugs = await _bugService.GetBugsByStatusAsync(status);
        
        var bugsDto = new List<object>();
        foreach (var bug in bugs)
        {
            var bugDto = new
            {
                id = bug.Id,
                description = bug.Description,
                action = bug.Action.ToString(),
                importance = bug.Importance.ToString(),
                status = (int)bug.Status,
                createdAt = bug.CreatedAt
            };
            bugsDto.Add(bugDto);
        }
        return bugsDto;
    }
}