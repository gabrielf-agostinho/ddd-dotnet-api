using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DDD.Domain.Entities;
using DDD.Application.DTOs.User;
using DDD.Application.Interfaces;

namespace DDD.Web.Controllers
{
  public class UsuariosController : BaseController<User, UserGetDTO, UserPostDTO, UserPutDTO>
  {
    public UsuariosController(IUserApp app) : base(app)
    {

    }

    [HttpPost]
    [AllowAnonymous]
    [Route("")]
    public override IActionResult Insert([FromBody] UserPostDTO userPostDTO)
    {
      try
      {
        return Ok(app.Insert(userPostDTO));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}