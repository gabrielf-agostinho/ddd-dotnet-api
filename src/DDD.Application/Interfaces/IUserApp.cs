using DDD.Domain.Entities;
using DDD.Application.DTOs.User;
using DDD.Application.Interfaces.Base;
using DDD.Application.DTOs.Auth;

namespace DDD.Application.Interfaces
{
  public interface IUserApp : IBaseApp<User, UserGetDTO, UserPostDTO, UserPutDTO>
  {
    User? Authenticate(AuthDTO authDTO);
    User? GetUserEntityById(int id);
    void Update(User user);
  }
}