using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MedHelp.Api;

/// <summary>
///   Middleware для проверки ключей API.
/// </summary>
public class ApiKeyMiddleware
{
  private const string ApiKeyHeaderName = "API_Key";
  private readonly RequestDelegate _next;
  private readonly string RequiredApiKey;

  public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
  {
    _next = next;
    RequiredApiKey = configuration.GetValue<string>("ApiSettings:ApiKey") ?? string.Empty;
  }
  
  public async Task InvokeAsync(HttpContext context)
  {
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
      await _next(context);
      return;
    }

    if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
    {
      context.Response.StatusCode = 404;
      await context.Response.WriteAsync("API Key was not provided.");
      return;
    }

    if (!RequiredApiKey.Equals(extractedApiKey))
    {
      context.Response.StatusCode = 404;
      await context.Response.WriteAsync("Unauthorized client.");
      return;
    }

    await _next(context);
  }
}
