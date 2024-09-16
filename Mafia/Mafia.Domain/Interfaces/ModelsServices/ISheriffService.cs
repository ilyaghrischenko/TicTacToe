namespace Mafia.Domain.Interfaces.ModelsServices;

public interface ISheriffService
{
    public Task<bool> Check(int userId);
    public Task<bool> Shoot(int userId);
}