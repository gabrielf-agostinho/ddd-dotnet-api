using DDD.Application.DTOs.Token;
using DDD.Domain.Entities;

namespace DDD.Application.Interfaces
{
  public interface ITokenGenerator
  {
    TokenDTO GetToken(User user);
    TokenDTO RefreshToken(RefreshTokenDTO refreshTokenDTO);
    void RevokeToken(int userId);
  }
}