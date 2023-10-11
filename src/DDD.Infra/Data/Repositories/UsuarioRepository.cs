using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Repositories;
using DDD.Infra.Data.Contexts;
using DDD.Infra.Data.Repositories.Base;

namespace DDD.Infra.Data.Repositories
{
  public class UserRepository : BaseRepository<User>, IUserRepository
  {
    public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
    {

    }
  }
}