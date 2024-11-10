using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Interfaces.DbModelsServices;

public interface IBugService
{
    Task SendBugAsync(Bug bug);
}