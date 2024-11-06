using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Domain.Interfaces;

public interface IAdminService : ICurrentUserId
{
    Task<List<User>?> GetAppealedUsersAsync();
    Task BlockUserAsync(int userId, string token);
    Task<bool> IsAdminAsync();
}