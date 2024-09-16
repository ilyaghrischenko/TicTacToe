namespace Mafia.Domain.Interfaces.ModelsServices;

public interface IMafiaService
{
    public Task<bool> Kill(int userId);
}