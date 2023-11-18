using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DDD.Infra.Data.Contexts;
using DDD.Domain.Entities.Base;
using DDD.Domain.Interfaces.Repositories.Base;

namespace DDD.Infra.Data.Repositories.Base
{
  public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
  {
    protected readonly DatabaseContext databaseContext;

    public BaseRepository(DatabaseContext databaseContext) : base()
    {
      this.databaseContext = databaseContext;
    }

    protected virtual Expression<Func<TEntity, bool>> Query(params object[] searchFilters)
    {
      throw new NotImplementedException();
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
      var dbset = databaseContext.Set<TEntity>();
      return dbset.ToList();
    }

    public virtual IEnumerable<TEntity> GetAll(int skip, int take, params object[] searchFilters)
    {
      var dbset = databaseContext.Set<TEntity>().Skip(skip).Take(take).Where(Query(searchFilters)).OrderByDescending(x => x.Id);
      return dbset.ToList();
    }

    public virtual int Count(params object[] searchFilters)
    {
      return databaseContext.Set<TEntity>().Where(Query(searchFilters)).Count();
    }

    public virtual TEntity GetById(int id)
    {
      return databaseContext.Set<TEntity>().Find(id)!;
    }

    public virtual int Insert(TEntity entity)
    {
      databaseContext.BeginTransaction();
      databaseContext.Set<TEntity>().Add(entity);
      databaseContext.SendChanges();
      return entity.Id!;
    }

    public virtual void Update(TEntity entity)
    {
      databaseContext.BeginTransaction();
      databaseContext.Set<TEntity>().Attach(entity);
      databaseContext.Entry(entity).State = EntityState.Modified;
      databaseContext.SendChanges();
    }

    public virtual void Delete(int id)
    {
      var entity = GetById(id);

      if (entity != null)
      {
        databaseContext.BeginTransaction();
        databaseContext.Set<TEntity>().Remove(entity);
        databaseContext.SendChanges();
      }
    }
  }
}