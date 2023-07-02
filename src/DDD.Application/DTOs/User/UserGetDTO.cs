namespace DDD.Application.DTOs.User
{
  public class UserGetDTO : BaseDTO
  {
    public string? Email { get; set; }
    public string? Name { get; set; }
  }
}