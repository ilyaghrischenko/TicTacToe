namespace Mafia.Domain.Interfaces.ModelsServices;

public interface ISlutService
{
    public Task<bool> Blocked(int userId);
}