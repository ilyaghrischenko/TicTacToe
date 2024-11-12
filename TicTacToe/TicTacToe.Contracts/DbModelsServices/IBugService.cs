using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.DbModelsServices;

public interface IBugService
{
    Task SendBugAsync(Bug bug);
}