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

    private string _GetRelations()
    {
      var properties = typeof(TEntity).GetProperties().ToList();

      var relations = "";
      foreach (var property in properties)
        if (property.PropertyType.IsSubclassOf(typeof(BaseEntity)))
          relations += relations != "" ? $".{property.Name}" : $"{property.Name}";

      return relations;
    }

    public IEnumerable<TEntity> GetAll()
    {
      var dbset = databaseContext.Set<TEntity>();

      var relations = _GetRelations();

      if (relations != "")
      {
        var query = dbset.Include(relations);
        return query.ToList();
      }
      else
        return dbset.ToList();
    }

    public IEnumerable<TEntity> GetAll(int skip, int take, params object[] searchFilters)
    {
      var dbset = databaseContext.Set<TEntity>().Skip(skip).Take(take).Where(Query(searchFilters)).OrderByDescending(x => x.Id);

      var relations = _GetRelations();

      if (relations != "")
      {
        var query = dbset.Include(relations);
        return query.ToList();
      }
      else
        return dbset.ToList();
    }

    public int Count(params object[] searchFilters)
    {
      return databaseContext.Set<TEntity>().Where(Query(searchFilters)).Count();
    }

    public TEntity GetById(int id)
    {
      var relations = _GetRelations();

      if (relations != "")
      {
        var dbset = databaseContext.Set<TEntity>();
        var query = dbset.Include(relations);
        return query.FirstOrDefault(c => c.Id == id)!;
      }
      else
        return databaseContext.Set<TEntity>().Find(id)!;
    }

    public int Insert(TEntity entity)
    {
      databaseContext.BeginTransaction();
      databaseContext.Set<TEntity>().Add(entity);
      databaseContext.SendChanges();
      return entity.Id!;
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