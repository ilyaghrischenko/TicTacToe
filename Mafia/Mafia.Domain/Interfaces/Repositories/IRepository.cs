namespace Mafia.Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    Task<List<T>?> GetAll();
    Task<T?> Get(int id);
    Task<bool> Add(T entity);
    Task<bool> Update(T entity, Action updateAction);
    Task<bool> Delete(int id);
}