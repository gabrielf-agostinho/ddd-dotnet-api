namespace DDD.Application.DTOs.Token
{
  public class TokenDTO
  {
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool Authenticated { get; set; }
  }
}