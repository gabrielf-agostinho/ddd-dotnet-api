using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DDD.Domain.Entities;
using DDD.Infra.Data.Mappings;

namespace DDD.Infra.Data.Contexts
{
  public class DatabaseContext : DbContext
  {
    public DbSet<User>? Users { get; set; }

    public IDbContextTransaction? Transaction { get; private set; }

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
      if (Transaction == null)
        Transaction = this.Database.BeginTransaction();

      return Transaction;
    }

    private void _Rollback()
    {
      if (Transaction != null)
        Transaction.Rollback();
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
      if (Transaction != null)
      {
        Transaction.Commit();
        Transaction.Dispose();
        Transaction = null;
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