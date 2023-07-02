using AutoMapper;
using DDD.Domain.Entities;
using DDD.Application.DTOs.User;

namespace DDD.Application
{
  public class EntityMapping : Profile
  {
    public EntityMapping()
    {
      _MapUsers();
    }

    private void _CreateMap<Entity, DTO>()
    {
      CreateMap<Entity, DTO>();
      CreateMap<DTO, Entity>();
    }

    private void _MapUsers()
    {
      _CreateMap<User, UserGetDTO>();
      _CreateMap<User, UserPostDTO>();
      _CreateMap<User, UserPutDTO>();
    }
  }
}