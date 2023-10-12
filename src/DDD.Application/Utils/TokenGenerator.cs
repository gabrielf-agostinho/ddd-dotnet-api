using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DDD.Application.Interfaces;
using DDD.Domain.Entities;
using DDD.Application.DTOs.Token;
using DDD.Application.Helpers.Exceptions.Token;

namespace DDD.Application.Utils
{
  public class TokenGenerator : ITokenGenerator
  {
    private readonly ITokenConfig _tokenConfig;
    private readonly IUserApp _userAppService;

    public TokenGenerator(IUserApp userApp, ITokenConfig tokenConfig)
    {
      _tokenConfig = tokenConfig;
      _userAppService = userApp;
    }

    public TokenDTO GetToken(User user)
    {
      var claims = _GenerateUserClaims(user);
      var token = _GenerateToken(claims);
      var refresh = _GenerateRefreshToken();

      _UpdateUserRefreshTokenAndExpiration(user, refresh);

      return _GetTokenDTO(token, refresh);
    }

    public TokenDTO RefreshToken(RefreshTokenDTO refreshTokenDTO)
    {
      var token = refreshTokenDTO.AccessToken;
      var refresh = refreshTokenDTO.RefreshToken;
      var principal = _GetClaimPrincipal(token!);

      if (!int.TryParse(principal.Identity!.Name, out var userId))
        throw new InvalidTokenException();

      User? user = _userAppService?.GetUserEntityById(userId);

      if (user is null || user.RefreshToken != refresh || user.ExpiresAt < DateTime.Now)
        throw new ExpiredTokenException();

      token = _GenerateToken(principal.Claims);
      refresh = _GenerateRefreshToken();

      _UpdateUserRefreshTokenAndExpiration(user, refresh);

      return _GetTokenDTO(token, refresh);
    }

    public void RevokeToken(int userId)
    {
      var user = _userAppService.GetUserEntityById(userId);

      if (user is not null)
      {
        user.RefreshToken = null;
        user.ExpiresAt = null;

        _userAppService.Update(user);
      }
    }

    private void _UpdateUserRefreshTokenAndExpiration(User user, string? refreshToken)
    {
      user.RefreshToken = refreshToken;
      user.ExpiresAt = DateTime.Now.AddDays(_tokenConfig.DaysToRefresh);

      _userAppService.Update(user);
    }

    private TokenDTO _GetTokenDTO(string token, string refresh)
    {
      return new TokenDTO
      {
        Authenticated = true,
        AccessToken = token,
        RefreshToken = refresh,
        CreatedAt = DateTime.Now,
        ExpiresAt = DateTime.Now.AddMinutes(_tokenConfig.MinutesToExpire)
      };
    }

    private string _GenerateRefreshToken()
    {
      var randomNumber = new byte[32];

      using (var randomNumberGenerator = RandomNumberGenerator.Create())
      {
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
      }
    }

    private string _GenerateToken(IEnumerable<Claim> claims)
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

    private ClaimsPrincipal _GetClaimPrincipal(string expiredToken)
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
        throw new SecurityTokenException("Invalid Token");

      return principal;
    }

    private List<Claim> _GenerateUserClaims(User user)
    {
      return new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email!)
      };
    }
  }
}