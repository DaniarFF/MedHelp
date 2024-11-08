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
      
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.AddControllers()
        .AddApi()
        .AddControllersAsServices();
      
      services.AddPostgreSqlStorage(options =>
      {
        options.UseNpgsql(config.GetConnectionString("ConnectionString"));
      });

      services.AddCoreIntegration();
    }

    public void Configure(IApplicationBuilder app)  
    {
      app.UseRouting();
      
      app.UseMiddleware<ApiKeyMiddleware>();  
      
      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });
      
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
