using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.Repository.Abstract
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll(bool ignoreDeleted = true);
        Task<T?> GetById(long id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> DeleteById(long id);
    }
}
