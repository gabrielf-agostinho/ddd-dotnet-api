using DDD.Domain.Entities;
using DDD.Application.DTOs.User;
using DDD.Application.Interfaces.Base;

namespace DDD.Application.Interfaces
{
  public interface IUserApp : IBaseApp<User, UserGetDTO, UserPostDTO, UserPutDTO>
  {

  }
}