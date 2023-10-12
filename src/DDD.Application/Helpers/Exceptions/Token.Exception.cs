namespace DDD.Application.Helpers.Exceptions.Token
{
  public class InvalidTokenException : Exception
  {
    public InvalidTokenException() : base("Invalid Token")
    {

    }
  }

  public class ExpiredTokenException : Exception
  {
    public ExpiredTokenException() : base("Expired Token, please perform a new authentication")
    {

    }
  }
}