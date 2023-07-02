using Microsoft.EntityFrameworkCore;
using DDD.Infra.Data.Contexts;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Repositories;

namespace DDD.Infra.Data.Repositories
{
  public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
  {
    protected readonly DatabaseContext databaseContext;

    public BaseRepository(DatabaseContext databaseContext) : base()
    {
      this.databaseContext = databaseContext;
    }

    public IEnumerable<TEntity> GetAll()
    {
      return databaseContext.Set<TEntity>().ToList();
    }

    public TEntity GetById(int id)
    {
      return databaseContext.Set<TEntity>().Find(id)!;
    }

    public int Insert(TEntity entity)
    {
      databaseContext.BeginTransaction();
      var id = databaseContext.Set<TEntity>().Add(entity).Entity.Id;
      databaseContext.SendChanges();
      return id;
    }

    public void Update(TEntity entity)
    {
      databaseContext.BeginTransaction();
      databaseContext.Set<TEntity>().Attach(entity);
      databaseContext.Entry(entity).State = EntityState.Modified;
      databaseContext.SendChanges();
    }

    public void Delete(int id)
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