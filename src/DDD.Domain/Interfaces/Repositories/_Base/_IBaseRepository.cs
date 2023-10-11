using DDD.Domain.Entities.Base;

namespace DDD.Domain.Interfaces.Repositories.Base
{
  public interface IBaseRepository<TEntity> where TEntity : BaseEntity
  {
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(int skip, int take, params object[] searchFilters);
    int Count(params object[] searchFilters);
    TEntity GetById(int id);
    int Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(int id);
  }
}