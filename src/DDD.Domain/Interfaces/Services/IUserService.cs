using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Services.Base;

namespace DDD.Domain.Interfaces.Services
{
  public interface IUserService : IBaseService<User>
  {
    User? Authenticate(string Email, string Password);
  }
}