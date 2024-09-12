using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> Get(string login);
}