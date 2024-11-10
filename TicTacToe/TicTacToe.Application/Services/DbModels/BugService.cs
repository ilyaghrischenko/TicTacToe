using TicTacToe.Contracts.Interfaces.DbModelsServices;
using TicTacToe.Contracts.Interfaces.Repositories;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Application.Services.DbModels;

public class BugService(IRepository<Bug> bugRepository) : IBugService
{
    private readonly IRepository<Bug> _bugRepository = bugRepository;
    
    public async Task SendBugAsync(Bug bug)
    {
        
        
        await _bugRepository.AddAsync(bug);
    }
}