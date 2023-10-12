using System.Security.Claims;

namespace DDD.Application.Interfaces
{
  public interface ITokenGenerator
  {
    string GenerateToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimPrincipal(string expiredToken);
  }
}