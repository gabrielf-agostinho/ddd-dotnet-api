namespace DDD.Domain.Entities
{
  public class User : BaseEntity
  {
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpiresAt { get; set; }
  }
}