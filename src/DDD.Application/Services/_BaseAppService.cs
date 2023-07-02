using AutoMapper;
using DDD.Application.Interfaces;
using DDD.Domain.Entities;
using DDD.Application.DTOs;
using DDD.Domain.Interfaces.Services;

namespace DDD.Application.Services
{
  public class BaseAppService<TEntity, TGetDTO, TPostDTO, TPutDTO> : IBaseApp<TEntity, TGetDTO, TPostDTO, TPutDTO>
    where TEntity : BaseEntity
    where TGetDTO : BaseDTO
    where TPostDTO : BaseDTO
    where TPutDTO : BaseDTO
  {
    protected readonly IBaseService<TEntity> service;
    protected readonly IMapper IMapper;

    public BaseAppService(IMapper IMapper, IBaseService<TEntity> service) : base()
    {
      this.IMapper = IMapper;
      this.service = service;
    }

    public IEnumerable<TGetDTO> GetAll()
    {
      return IMapper.Map<IEnumerable<TGetDTO>>(service.GetAll());
    }

    public TGetDTO GetById(int id)
    {
      return IMapper.Map<TGetDTO>(service.GetById(id));
    }

    public int Insert(TPostDTO dto)
    {
      return service.Insert(IMapper.Map<TEntity>(dto));
    }

    public void Update(TPutDTO dto)
    {
      service.Update(IMapper.Map<TEntity>(dto));
    }

    public void Delete(int id)
    {
      service.Delete(id);
    }
  }
}