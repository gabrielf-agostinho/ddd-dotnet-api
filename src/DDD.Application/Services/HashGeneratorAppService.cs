using System.Security.Cryptography;
using System.Text;

namespace DDD.Application.Services
{
  public static class HashGeneratorAppService
  {
    public static string GenerateHash(string text)
    {
      var sha256 = SHA256.Create();
      var input = Encoding.UTF8.GetBytes(text);
      var hash = sha256.ComputeHash(input);

      return BitConverter.ToString(hash);
    }
  }
}