using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetAsync(string login);
    public Task<User?> GetAdminAsync();
}