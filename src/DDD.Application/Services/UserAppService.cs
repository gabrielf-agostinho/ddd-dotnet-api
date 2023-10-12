using AutoMapper;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Services;
using DDD.Application.DTOs.User;
using DDD.Application.Interfaces;
using DDD.Application.Services.Base;
using DDD.Application.DTOs.Auth;

namespace DDD.Application.Services
{
  public class UserAppService : BaseAppService<User, UserGetDTO, UserPostDTO, UserPutDTO>, IUserApp
  {
    protected readonly IUserService _userService;

    public UserAppService(IMapper IMapper, IUserService service) : base(IMapper, service)
    {
      _userService = service;
    }

    public User? Authenticate(AuthDTO authDTO)
    {
      if (authDTO.Email is null || authDTO.Password is null)
        throw new Exception();

      return _userService.Authenticate(authDTO.Email, authDTO.Password);
    }

    public User? GetUserEntityById(int id)
    {
      return _userService.GetById(id);
    }

    public void Update(User user)
    {
      _userService.Update(user);
    }
  }
}