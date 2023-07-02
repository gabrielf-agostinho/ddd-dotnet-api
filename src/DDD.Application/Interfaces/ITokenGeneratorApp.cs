using System.Security.Claims;

namespace DDD.Application.Interfaces
{
  public interface ITokenGeneratorApp
  {
    string GenerateToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimPrincipal(string expiredToken);
  }
}