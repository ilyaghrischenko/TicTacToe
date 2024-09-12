namespace Domain.Interfaces.Models;

public interface IMafiaService
{
    public Task<bool> Kill(int userId);
}