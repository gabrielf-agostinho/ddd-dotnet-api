using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DDD.Application.Interfaces;

namespace DDD.Web.Configurations
{
  public sealed class TokenConfig : ITokenConfig
  {
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public string? Secret { get; set; }
    public int MinutesToExpire { get; set; }
    public int DaysToRefresh { get; set; }
  }

  public static class TokenConfigurator
  {
    public static TokenConfig ReadTokenConfig(this IConfiguration configuration)
    {
      var section = configuration.GetSection("TokenConfig");
      var configurator = new ConfigureFromConfigurationOptions<TokenConfig>(section);
      var tokenConfiguration = new TokenConfig();

      configurator.Configure(tokenConfiguration);

      return tokenConfiguration;
    }

    public static void ConfigureJWT(this IServiceCollection services, TokenConfig tokenConfig)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Secret!));

      services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidIssuer = tokenConfig.Issuer,
          ValidAudience = tokenConfig.Audience,
          IssuerSigningKey = key,
          ValidateIssuerSigningKey = true
        };
      });
    }
  }
}