using System.Reflection;
using MedHelp.Api.Api;
using Microsoft.Extensions.DependencyInjection;

namespace MedHelp.Api;

/// <summary>
///   Entry point для регистрации контроллеров API
/// </summary>
public static class Entry
{
  public static IMvcBuilder AddApi(this IMvcBuilder builder)
  {
    builder.AddApplicationPart(Assembly.GetAssembly(typeof(DrugsController)));
    builder.AddApplicationPart(Assembly.GetAssembly(typeof(DiseasesController)));
    return builder;
  }
}
