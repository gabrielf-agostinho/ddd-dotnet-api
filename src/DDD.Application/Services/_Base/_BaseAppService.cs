using AutoMapper;
using DDD.Application.Interfaces.Base;
using DDD.Domain.Entities.Base;
using DDD.Application.DTOs.Base;
using DDD.Domain.Interfaces.Services.Base;
using DDD.Application.Utils;

namespace DDD.Application.Services.Base
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

    public virtual IEnumerable<TGetDTO> GetAll()
    {
      return IMapper.Map<IEnumerable<TGetDTO>>(service.GetAll());
    }

    public virtual Pagination<TGetDTO> GetAll(bool paginate, int skip, int take, params object[] filtros)
    {
      if (paginate)
        return new Pagination<TGetDTO>(
          service.Count(filtros),
          IMapper.Map<IEnumerable<TGetDTO>>(service.GetAll(skip, take, filtros))
        );

      IEnumerable<TGetDTO> itens = IMapper.Map<IEnumerable<TGetDTO>>(service.GetAll());
      return new Pagination<TGetDTO>(itens.Count(), itens);
    }

    public virtual TGetDTO GetById(int id)
    {
      return IMapper.Map<TGetDTO>(service.GetById(id));
    }

    public virtual int Insert(TPostDTO dto)
    {
      return service.Insert(IMapper.Map<TEntity>(dto));
    }

    public virtual void Update(TPutDTO dto)
    {
      if (dto.Id is null)
        return;

      TEntity entity = service.GetById((int)dto.Id!);
      service.Update(IMapper.Map(dto, entity));
    }

    public virtual void Delete(int id)
    {
      service.Delete(id);
    }
  }
}