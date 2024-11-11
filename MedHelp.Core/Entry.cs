using MedHelp.Core.Abstractions;
using MedHelp.Core.Services;
using MedHelp.DBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Core
{
  public static class Entry
  {
    /// <summary>
    /// Внедрение зависимостей для работы с Core.
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddCoreIntegration(this IServiceCollection serviceCollection)
    {
      serviceCollection.AddScoped<IDrugService, DrugService>();
      serviceCollection.AddScoped<IUserService, UserService>();
      serviceCollection.AddScoped<IDiseaseService, DiseaseService>();
      serviceCollection.AddScoped<IDocumentService, DocumentService>();

      return serviceCollection;
    }
  }
}
