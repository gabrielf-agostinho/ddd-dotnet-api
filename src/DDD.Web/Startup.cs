using Microsoft.EntityFrameworkCore;
using DDD.Infra.Data.Contexts;
using DDD.Infra.IoC;
using DDD.Application;
using DDD.Web.Configurations;

class Startup
{
  public IConfiguration Configuration { get; }

  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services)
  {
    var tokenConfig = Configuration.ReadTokenConfig();
    services.ConfigureJWT(tokenConfig);

    services.AddControllers();
    services.AddDbContext<DatabaseContext>(db => db.UseNpgsql(Configuration.GetConnectionString("database")));

    services.AddCors(CorsConfig.DefaultConfiguration);
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    DependencyInjector.Register(services);

    services.AddAutoMapper(x => x.AddProfile(new EntityMapping()));

    services.AddMvc();
  }

  public void Configure(WebApplication app, IWebHostEnvironment environment)
  {
    if (environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseCors(CorsConfig.POLICY_NAME);

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
  }
}