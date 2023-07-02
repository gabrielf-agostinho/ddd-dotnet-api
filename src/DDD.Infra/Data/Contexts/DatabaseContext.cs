using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DDD.Domain.Entities;
using DDD.Infra.Data.Mappings;

namespace DDD.Infra.Data.Contexts
{
  public class DatabaseContext : DbContext
  {
    public DbSet<User>? Users { get; set; }

    public IDbContextTransaction? transaction { get; private set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
      if (Database.GetPendingMigrations().Count() > 0)
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      
    }

    public IDbContextTransaction BeginTransaction()
    {
      if (transaction == null)
        transaction = this.Database.BeginTransaction();

      return transaction;
    }

    private void _Rollback()
    {
      if (transaction != null)
        transaction.Rollback();
    }

    private void _SaveChanges()
    {
      try
      {
        ChangeTracker.DetectChanges();
        SaveChanges();
      }
      catch (Exception e)
      {
        _Rollback();
        throw new Exception(e.Message);
      }
    }

    private void _Commit()
    {
      if (transaction != null)
      {
        transaction.Commit();
        transaction.Dispose();
        transaction = null;
      }
    }

    public void SendChanges()
    {
      _SaveChanges();
      _Commit();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new UserMap());
    }
  }
}