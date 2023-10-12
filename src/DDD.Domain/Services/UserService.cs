using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Repositories;
using DDD.Domain.Interfaces.Services;
using DDD.Domain.Services.Base;

namespace DDD.Domain.Services
{
  public class UserService : BaseService<User>, IUserService
  {
    protected readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) : base(userRepository)
    {
      _userRepository = userRepository;
    }

    public User? Authenticate(string Email, string Password)
    {
      return _userRepository.Authenticate(Email, Password);
    }
  }
}