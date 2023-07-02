using DDD.Domain.Entities;

namespace DDD.Domain.Interfaces.Services
{
  public interface IBaseService<TEntity> where TEntity : BaseEntity
  {
    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);
    int Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(int id);
  }
}