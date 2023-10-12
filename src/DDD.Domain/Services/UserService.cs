using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Repositories;
using DDD.Domain.Interfaces.Services;
using DDD.Domain.Services.Base;

namespace DDD.Domain.Services
{
  public class UserService : BaseService<User>, IUserService
  {
    public UserService(IUserRepository userRepository) : base(userRepository)
    {
      
    }
  }
}