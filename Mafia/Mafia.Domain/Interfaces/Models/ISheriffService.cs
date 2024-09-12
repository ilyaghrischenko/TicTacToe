namespace Mafia.Domain.Interfaces.Models;

public interface ISheriffService
{
    public Task<bool> Check(int userId);
    public Task<bool> Shoot(int userId);
}