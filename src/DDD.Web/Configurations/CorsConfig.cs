using Microsoft.AspNetCore.Cors.Infrastructure;

namespace DDD.Web.Configurations
{
  public static class CorsConfig
  {
    public const string POLICY_NAME = "DEFAULT_POLICY";

    public static void DefaultConfiguration(CorsOptions options)
    {
      options.AddPolicy(POLICY_NAME, builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins(new string[]
        {
          "http://localhost:4200"    
        })
      );
    }
  }
}