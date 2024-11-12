using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Contracts.DbModelsServices;

public interface IBugService
{
    Task SendBugAsync(Bug bug);
    Task<List<Bug>> GetAllBugsAsync();
    Task<List<Bug>> GetBugsByStatusAsync(int status);
}