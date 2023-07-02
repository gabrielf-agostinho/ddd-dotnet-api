using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Repositories;
using DDD.Domain.Interfaces.Services;

namespace DDD.Domain.Services
{
  public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
  {
    protected readonly IBaseRepository<TEntity> repository;

    public BaseService(IBaseRepository<TEntity> repository)
    {
      this.repository = repository;
    }

    public IEnumerable<TEntity> GetAll()
    {
      return repository.GetAll();
    }

    public TEntity GetById(int id)
    {
      return repository.GetById(id);
    }

    public int Insert(TEntity entity)
    {
      return repository.Insert(entity);
    }

    public void Update(TEntity entity)
    {
      repository.Update(entity);
    }

    public void Delete(int id)
    {
      repository.Delete(id);
    }
  }
}