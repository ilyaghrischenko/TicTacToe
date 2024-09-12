namespace Domain.Interfaces.Models;

public interface IDoctorService
{
    public Task<bool> Cure(int selfId, int userId);
}