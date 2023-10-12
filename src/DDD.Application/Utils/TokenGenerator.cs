using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DDD.Application.Interfaces;
using DDD.Domain.Entities;

namespace DDD.Application.Utils
{
  public class TokenGenerator : ITokenGenerator
  {
    private ITokenConfig _tokenConfig;

    public TokenGenerator(ITokenConfig tokenConfig)
    {
      _tokenConfig = tokenConfig;
    }

    public string GenerateRefreshToken()
    {
      var randomNumber = new byte[32];

      using (var randomNumberGenerator = RandomNumberGenerator.Create())
      {
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
      }
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret!));
      var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

      var options = new JwtSecurityToken(
        issuer: _tokenConfig.Issuer,
        audience: _tokenConfig.Audience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(_tokenConfig.MinutesToExpire),
        signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(options);
    }

    public ClaimsPrincipal GetClaimPrincipal(string expiredToken)
    {
      var validationParameters = new TokenValidationParameters
      {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret!)),
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
      };

      var handler = new JwtSecurityTokenHandler();

      SecurityToken securityToken;

      var principal = handler.ValidateToken(expiredToken, validationParameters, out securityToken);

      var jwtSecurityToken = securityToken as JwtSecurityToken;

      if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
        throw new SecurityTokenException("Token inválido");

      return principal;
    }

    public List<Claim> GenerateUserClaims(User user)
    {
      return new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email!)
      };
    }
  }
}