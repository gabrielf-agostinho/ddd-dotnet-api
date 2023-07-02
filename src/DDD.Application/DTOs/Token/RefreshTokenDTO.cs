namespace DDD.Application.DTOs.Token
{
  public class RefreshTokenDTO
  {
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
  }
}