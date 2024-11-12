using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>?> GetAllAsync();
    Task<T?> GetAsync(int id);
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity, Action updateAction);
    Task<bool> DeleteAsync(int id);
}