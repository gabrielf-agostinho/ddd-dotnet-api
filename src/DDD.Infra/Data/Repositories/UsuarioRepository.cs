using System.Linq.Expressions;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Repositories;
using DDD.Infra.Data.Contexts;
using DDD.Infra.Data.Repositories.Base;
using DDD.Application.Utils;

namespace DDD.Infra.Data.Repositories
{
  public class UserRepository : BaseRepository<User>, IUserRepository
  {
    public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
    {

    }

    protected override Expression<Func<User, bool>> Query(params object[] searchFilters)
    {
      string interval = searchFilters.Get<string>(0)!;

      Expression<Func<User, bool>> searchQuery = usuario =>
          usuario.Name!.ToLower().Contains(interval) ||
          usuario.Email!.ToLower().Contains(interval);

      return searchQuery;
    }

    public User? Authenticate(string Email, string Password)
    {
      return databaseContext.Users!.FirstOrDefault(u => u.Email == Email && u.Password == Password);
    }
  }
}