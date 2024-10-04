using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces;

public interface IAdminService
{
    Task<List<User>?> GetAppealedUsers();
    Task BlockUser(int userId);
}