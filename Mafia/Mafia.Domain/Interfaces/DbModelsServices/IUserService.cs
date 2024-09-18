using Mafia.Domain.DbModels;

namespace Mafia.Domain.Interfaces.DbModelsServices;

public interface IUserService
{
    public Task<bool> AddFriend(User user, User friend);
}