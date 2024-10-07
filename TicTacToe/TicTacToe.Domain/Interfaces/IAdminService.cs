using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Domain.Interfaces;

public interface IAdminService : ICurrentUserId
{
    Task<List<User>?> GetAppealedUsers();
    Task BlockUser(int userId);
    Task<bool> IsAdmin();
}