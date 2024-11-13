using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Services.DbModels;

public class BugService(IRepository<Bug> bugRepository) : IBugService
{
    private readonly IRepository<Bug> _bugRepository = bugRepository;
    
    public async Task SendBugAsync(Bug bug)
    {
        var allBugs = await _bugRepository.GetAllAsync();
        var existingBug = allBugs?.FirstOrDefault(b =>
            b.Action == bug.Action &&
            b.Importance == bug.Importance &&
            b.Description == bug.Description);
        
        if (existingBug != null)
        {
            throw new ArgumentException("Bug already exists");
        }
        
        await _bugRepository.AddAsync(bug);
    }
    
    public async Task<List<Bug>> GetAllBugsAsync()
    {
        var allBugs = await _bugRepository.GetAllAsync() ?? new();
        return allBugs;
    }
    
    public async Task<List<Bug>> GetBugsByStatusAsync(int status)
    {
        var allBugs = await _bugRepository.GetAllAsync();
        if (allBugs == null)
        {
            return new();
        }
        
        var bugs = allBugs
            .Where(b => (int)b.Status == status)
            .ToList();
        return bugs;
    }
}