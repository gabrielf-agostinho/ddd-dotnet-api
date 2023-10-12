using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DDD.Application.DTOs.Auth;
using DDD.Application.DTOs.Token;
using DDD.Application.Utils;
using DDD.Application.Interfaces;
using DDD.Web.Configurations;

namespace DDD.Web.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly IUserApp _userAppService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserApp userApp, IConfiguration configuration)
    {
      _userAppService = userApp;
      _configuration = configuration;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Auth([FromBody] AuthDTO authDTO)
    {
      try
      {
        var user = _userAppService.Authenticate(authDTO);

        if (user is null)
          return Unauthorized();

        var _tokenGenerator = new TokenGenerator(_userAppService, _configuration.ReadTokenConfig());
        return Ok(_tokenGenerator.GetToken(user));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Route("refresh")]
    public IActionResult Refresh([FromBody] RefreshTokenDTO refreshTokenDTO)
    {
      try
      {
        var _tokenGenerator = new TokenGenerator(_userAppService, _configuration.ReadTokenConfig());
        return Ok(_tokenGenerator.RefreshToken(refreshTokenDTO));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPatch]
    [Authorize]
    [Route("revoke")]
    public IActionResult Revoke()
    {
      try
      {
        var userId = int.Parse(User.Identity!.Name!);

        var _tokenGenerator = new TokenGenerator(_userAppService, _configuration.ReadTokenConfig());
        
        _tokenGenerator.RevokeToken(userId);

        return Ok();
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}