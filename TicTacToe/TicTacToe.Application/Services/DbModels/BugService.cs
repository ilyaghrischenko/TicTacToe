using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Domain.DbModels;

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
}