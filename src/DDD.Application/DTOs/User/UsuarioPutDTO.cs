using DDD.Application.Services;

namespace DDD.Application.DTOs.User
{
  public class UserPutDTO : BaseDTO
  {
    public string? Email { get; set; }
    private string? _password;
    public string? Password
    {
      get
      {
        return _password;
      }
      set
      {
        _password = HashGeneratorAppService.GenerateHash(value!);
      }
    }
    public string? Name { get; set; }
  }
}