using Domain.DbModels;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> Get(string login);
}