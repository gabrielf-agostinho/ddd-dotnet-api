using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Repositories.Base;

namespace DDD.Domain.Interfaces.Repositories
{
  public interface IUserRepository : IBaseRepository<User>
  {
    User? Authenticate(string Email, string Password);
  }
}