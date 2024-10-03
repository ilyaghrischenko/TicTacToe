namespace TicTacToe.Domain.Interfaces;

public interface IAdminService
{
    Task BlockUser(int userId);
}