using System.Threading.Tasks;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetAsync(string login);
    public Task<User?> GetAdminAsync();
}