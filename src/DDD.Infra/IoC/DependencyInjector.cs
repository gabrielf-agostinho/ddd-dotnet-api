using Microsoft.Extensions.DependencyInjection;
using DDD.Application.Interfaces;
using DDD.Application.Services;
using DDD.Application.DTOs;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces.Repositories;
using DDD.Domain.Interfaces.Services;
using DDD.Domain.Services;
using DDD.Infra.Data.Repositories;

namespace DDD.Infra.IoC
{
  public class DependencyInjector
  {
    public static void Register(IServiceCollection services)
    {
      _RegisterApplication(services);
      _RegisterDomain(services);
      _RegisterRepository(services);
    }

    private static void _RegisterApplication(IServiceCollection services)
    {
      services.AddScoped(typeof(IBaseApp<BaseEntity, BaseDTO, BaseDTO, BaseDTO>), typeof(BaseAppService<BaseEntity, BaseDTO, BaseDTO, BaseDTO>));

      services.AddScoped<IUserApp, UserAppService>();
    }

    private static void _RegisterDomain(IServiceCollection services)
    {
      services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

      services.AddScoped<IUserService, UserService>();
    }

    private static void _RegisterRepository(IServiceCollection services)
    {
      services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

      services.AddScoped<IUserRepository, UserRepository>();
    }
  }
}