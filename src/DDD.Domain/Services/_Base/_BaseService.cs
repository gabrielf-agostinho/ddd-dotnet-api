using DDD.Domain.Entities.Base;
using DDD.Domain.Interfaces.Repositories.Base;
using DDD.Domain.Interfaces.Services.Base;

namespace DDD.Domain.Services.Base
{
  public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
  {
    protected readonly IBaseRepository<TEntity> repository;

    public BaseService(IBaseRepository<TEntity> repository)
    {
      this.repository = repository;
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
      return repository.GetAll();
    }

    public virtual IEnumerable<TEntity> GetAll(int skip, int take, params object[] filtros)
    {
      return repository.GetAll(skip, take, filtros);
    }

    public virtual int Count(params object[] filtros)
    {
      return repository.Count(filtros);
    }

    public virtual TEntity GetById(int id)
    {
      return repository.GetById(id);
    }

    public virtual int Insert(TEntity entity)
    {
      return repository.Insert(entity);
    }

    public virtual void Update(TEntity entity)
    {
      repository.Update(entity);
    }

    public virtual void Delete(int id)
    {
      repository.Delete(id);
    }
  }
}