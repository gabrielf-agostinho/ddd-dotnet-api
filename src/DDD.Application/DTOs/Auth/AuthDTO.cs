using DDD.Application.Utils;

namespace DDD.Application.DTOs.Auth
{
  public class AuthDTO
  {
    public string? Email { get; set; }
    private string? _password;
    public string? Password {
      get 
      {
         return _password;
      }
      set 
      {
        _password = HashGenerator.GenerateHash(value!);
      }
    }
  }
}