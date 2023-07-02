using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DDD.Application.DTOs.Auth;
using DDD.Application.DTOs.Token;
using DDD.Application.Services;
using DDD.Infra.Data.Contexts;
using DDD.Web.Configurations;

namespace DDD.Web.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly DatabaseContext _databaseContext;
    private readonly TokenGeneratorAppService _tokenGeneratorAppService;
    private readonly TokenConfig _tokenConfig;

    public AuthController(DatabaseContext databaseContext, TokenGeneratorAppService tokenGeneratorAppService, TokenConfig tokenConfig)
    {
      _databaseContext = databaseContext;
      _tokenGeneratorAppService = tokenGeneratorAppService;
      _tokenConfig = tokenConfig;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Auth([FromBody] AuthDTO authDTO)
    {
      var user = _databaseContext.Users!.FirstOrDefault(u => u.Email == authDTO.Email && u.Password == authDTO.Password);

      if (user is null)
        return Unauthorized();

      var claims = _tokenGeneratorAppService.GenerateUserClaims(user);
      var token = _tokenGeneratorAppService.GenerateToken(claims);
      var refresh = _tokenGeneratorAppService.GenerateRefreshToken();

      user.RefreshToken = refresh;
      user.ExpiresAt = DateTime.Now.AddDays(_tokenConfig.DaysToRefresh);

      _databaseContext.SaveChanges();

      var createdAt = DateTime.Now;
      var expiresAt = createdAt.AddMinutes(_tokenConfig.MinutesToExpire);

      var tokenDTO = new TokenDTO
      {
        Authenticated = true,
        AccessToken = token,
        RefreshToken = refresh,
        CreatedAt = createdAt,
        ExpiresAt = expiresAt
      };

      return Ok(tokenDTO);
    }

    [HttpPost]
    [Route("refresh")]
    public IActionResult Refresh([FromBody] RefreshTokenDTO refreshTokenDTO)
    {
      var token = refreshTokenDTO.AccessToken;
      var refresh = refreshTokenDTO.RefreshToken;
      var principal = _tokenGeneratorAppService.GetClaimPrincipal(token!);

      if (!int.TryParse(principal.Identity!.Name, out var userId))
        return Unauthorized("Invalid token");

      var user = _databaseContext.Users!.Find(userId);

      if (user is null || user.RefreshToken != refresh || user.ExpiresAt < DateTime.Now)
        return Unauthorized("Please perform a new authentication");

      token = _tokenGeneratorAppService.GenerateToken(principal.Claims);
      refresh = _tokenGeneratorAppService.GenerateRefreshToken();

      user.RefreshToken = refresh;
      user.ExpiresAt = DateTime.Now.AddDays(_tokenConfig.DaysToRefresh);

      _databaseContext.SaveChanges();

      var createdAt = DateTime.Now;
      var expiresAt = createdAt.AddMinutes(_tokenConfig.MinutesToExpire);

      var tokenDTO = new TokenDTO
      {
        Authenticated = true,
        AccessToken = token,
        RefreshToken = refresh,
        CreatedAt = createdAt,
        ExpiresAt = expiresAt
      };

      return Ok(tokenDTO);
    }

    [HttpPatch]
    [Authorize]
    [Route("revoke")]
    public IActionResult Revoke()
    {
      var userId = int.Parse(User.Identity!.Name!);
      var usuario = _databaseContext.Users!.Find(userId);

      if (usuario is not null)
      {
        usuario.RefreshToken = null;
        usuario.ExpiresAt = null;

        _databaseContext.SaveChanges();
      }

      return Ok();
    }
  }
}