using Microsoft.AspNetCore.Mvc;

namespace UPV.Web.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class HealthController : Controller
  {
    [HttpGet]
    [Route("")]
    public IActionResult HealthCheck()
    {
      return Ok();
    }
  }
}