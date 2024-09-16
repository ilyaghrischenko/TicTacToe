namespace Mafia.Domain.Interfaces.ModelsServices;

public interface IDoctorService
{
    public Task<bool> Cure(int selfId, int userId);
}