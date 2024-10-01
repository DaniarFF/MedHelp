using MedHelp.Core.Abstractions;
using MedHelp.Core.Services;
using MedHelp.DBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.EventsArgs;

namespace MedHelp
{
  internal class Program
  {
    static async Task Main(string[] args)
    {
      IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
      string botToken = config.GetValue<string>("Telegram:Token");

      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddScopedBotHandlers();
      builder.Services.AddLogging();
      builder.Services.AddScoped<IDiseaseRepository, DiseaseRepository>();
      builder.Services.AddScoped<IDrugRepository, DrugRepository>();
      builder.Services.AddScoped<IUserRepository, UserRepository>();
      builder.Services.AddScoped<IDrugService, DrugService>();
      builder.Services.AddScoped<IUserService, UserService>();
      builder.Services.AddScoped<DiseaseService>();
      builder.Services.AddScoped<CreatePDFService>();

      builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseNpgsql(
        builder.Configuration.GetConnectionString("ConnectionString")));

      var app = builder.Build();

      var serviceProvider = app.Services.GetService<IServiceProvider>();

      var telegram = new PRBotBuilder(botToken)
                          .SetServiceProvider(serviceProvider)
                          .Build();

      telegram.Events.OnCommonLog += Telegram_OnLogCommon;
      telegram.Events.OnErrorLog += Telegram_OnLogError;

      await telegram.Start();

      app.Run();

      async Task Telegram_OnLogError(ErrorLogEventArgs e)
      {
        Console.WriteLine(e.Exception.Message);
      }

      async Task Telegram_OnLogCommon(CommonLogEventArgs e)
      {
        Console.WriteLine($"{e.Message}");
      }
    }
  }
}
