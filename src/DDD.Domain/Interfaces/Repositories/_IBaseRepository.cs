using DDD.Domain.Entities;

namespace DDD.Domain.Interfaces.Repositories
{
  public interface IBaseRepository<TEntity> where TEntity : BaseEntity
  {
    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);
    int Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(int id);
  }
}