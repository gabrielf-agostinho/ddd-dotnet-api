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
  }
}