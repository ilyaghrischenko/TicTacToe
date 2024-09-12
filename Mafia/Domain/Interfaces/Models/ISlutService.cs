namespace Domain.Interfaces.Models;

public interface ISlutService
{
    public Task<bool> Blocked(int userId);
}