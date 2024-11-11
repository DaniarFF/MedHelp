using MedHelp.Core.Abstractions;
using MedHelp.Core.Services;
using MedHelp.DBase;
using MedHelp.TelegramBot;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
      
      var startup = new Startup(builder.Configuration);

      startup.ConfigureServices(builder.Services);

      var app = builder.Build();
      
      startup.Configure(app);

      var serviceProvider = app.Services.GetService<IServiceProvider>();

      var telegram = new PRBotBuilder(botToken)
        .SetServiceProvider(serviceProvider)
        .Build();

      telegram.Events.OnCommonLog += Telegram_OnLogCommon;
      telegram.Events.OnErrorLog += Telegram_OnLogError;

      await telegram.Start();

      await app.RunAsync();

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