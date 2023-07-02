using DDD.Domain.Entities;
using DDD.Application.DTOs;

namespace DDD.Application.Interfaces
{
  public interface IBaseApp<TEntity, TGetDTO, TPostDTO, TPutDTO>
    where TEntity : BaseEntity
    where TGetDTO : BaseDTO
    where TPostDTO : BaseDTO
    where TPutDTO : BaseDTO
  {
    IEnumerable<TGetDTO> GetAll();
    TGetDTO GetById(int id);
    int Insert(TPostDTO dto);
    void Update(TPutDTO dto);
    void Delete(int id);
  }
}