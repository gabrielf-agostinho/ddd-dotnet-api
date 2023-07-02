using DDD.Domain.Entities;
using DDD.Application.DTOs.User;

namespace DDD.Application.Interfaces
{
  public interface IUserApp : IBaseApp<User, UserGetDTO, UserPostDTO, UserPutDTO>
  {

  }
}