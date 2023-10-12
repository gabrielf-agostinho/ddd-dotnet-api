using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DDD.Domain.Entities;
using DDD.Application.DTOs.User;
using DDD.Application.Interfaces;
using DDD.Web.Controllers.Base;

namespace DDD.Web.Controllers
{
  public class UsersController : BaseController<User, UserGetDTO, UserPostDTO, UserPutDTO>
  {
    public UsersController(IUserApp app) : base(app)
    {

    }

    [HttpPost]
    [AllowAnonymous]
    [Route("")]
    public override IActionResult Insert([FromBody] UserPostDTO userPostDTO)
    {
      return base.Insert(userPostDTO);
    }
  }
}