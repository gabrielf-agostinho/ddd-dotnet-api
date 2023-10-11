using DDD.Domain.Entities.Base;
using DDD.Application.DTOs.Base;
using DDD.Application.Utils;

namespace DDD.Application.Interfaces.Base
{
  public interface IBaseApp<TEntity, TGetDTO, TPostDTO, TPutDTO>
    where TEntity : BaseEntity
    where TGetDTO : BaseDTO
    where TPostDTO : BaseDTO
    where TPutDTO : BaseDTO
  {
    IEnumerable<TGetDTO> GetAll();
    Pagination<TGetDTO> GetAll(bool paginate, int skip, int take, params object[] searchFilters);
    TGetDTO GetById(int id);
    int Insert(TPostDTO dto);
    void Update(TPutDTO dto);
    void Delete(int id);
  }
}