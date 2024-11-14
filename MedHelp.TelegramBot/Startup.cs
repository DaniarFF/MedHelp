using MedHelp.DBase;
using MedHelp.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Extensions;
using PRTelegramBot.Core;
using System;
using MedHelp.Api;
using Microsoft.OpenApi.Models;
using PRTelegramBot.Models.EventsArgs;

namespace MedHelp.TelegramBot
{
  /// <summary>
  ///  Startup class для конфигурирования приложения 
  /// </summary>
  public class Startup
  {
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

      services.AddScopedBotHandlers();
      services.AddLogging();
      services.AddControllers();
      services.AddPostgreSqlStorage(options =>
      {
        options.UseNpgsql(config.GetConnectionString("ConnectionString"));
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
      });
      
      services.AddCoreIntegration();
    }

    public void Configure(IApplicationBuilder app)  
    {
      app.UseRouting();
      app.UseMiddleware<ApiKeyMiddleware>();
      
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
      });
      
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
