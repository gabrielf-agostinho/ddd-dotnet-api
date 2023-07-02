using Microsoft.AspNetCore.Mvc;
using DDD.Domain.Entities;
using DDD.Application.DTOs;
using DDD.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DDD.Web.Controllers
{
  [Authorize]
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class BaseController<TEntity, TGetDTO, TPostDTO, TPutDTO> : Controller
    where TEntity : BaseEntity
    where TGetDTO : BaseDTO
    where TPostDTO : BaseDTO
    where TPutDTO : BaseDTO
  {
    readonly protected IBaseApp<TEntity, TGetDTO, TPostDTO, TPutDTO> app;

    public BaseController(IBaseApp<TEntity, TGetDTO, TPostDTO, TPutDTO> app)
    {
      this.app = app;
    }

    [HttpGet]
    [Route("")]
    public virtual IActionResult GetAll()
    {
      try
      {
        IEnumerable<TGetDTO> items = app.GetAll();
        return Ok(items);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    [Route("{id:int}")]
    public virtual IActionResult GetById(int id)
    {
      try
      {
        TGetDTO item = app.GetById(id);

        if (item is null)
          return NotFound();
          
        return Ok(item);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Route("")]
    public virtual IActionResult Insert([FromBody] TPostDTO dto)
    {
      try
      {
        return Ok(app.Insert(dto));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut]
    [Route("")]
    public virtual IActionResult Update([FromBody] TPutDTO dto)
    {
      try
      {
        app.Update(dto);
        return Ok();
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public virtual IActionResult Delete(int id)
    {
      try
      {
        app.Delete(id);
        return Ok();
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}