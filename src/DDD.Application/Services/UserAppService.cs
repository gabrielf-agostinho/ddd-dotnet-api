using AutoMapper;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Services;
using DDD.Application.DTOs.User;
using DDD.Application.Interfaces;

namespace DDD.Application.Services
{
  public class UserAppService : BaseAppService<User, UserGetDTO, UserPostDTO, UserPutDTO>, IUserApp
  {
    public UserAppService(IMapper IMapper, IUserService service) : base(IMapper, service)
    {

    }
  }
}