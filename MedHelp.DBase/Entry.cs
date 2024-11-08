using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.DBase
{
  /// <summary>
  /// Класс для регистрации зависимостей для работы с БД PostgreSQL
  /// </summary>
  public static class Entry
  {
    private static readonly Action<DbContextOptionsBuilder> DefaultOptionsAction = (_) => { };

    /// <summary>
    /// Добавления зависимостей для работы с БД
    /// </summary>
    /// <param name="serviceCollection">serviceCollection</param>
    /// <param name="optionsAction">optionsAction</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddPostgreSqlStorage(this IServiceCollection serviceCollection,
        Action<DbContextOptionsBuilder> optionsAction)
    {
      serviceCollection.AddDbContext<AppDbContext>(optionsAction ?? DefaultOptionsAction);
      serviceCollection.AddScoped<IDiseaseRepository, DiseaseRepository>();
      serviceCollection.AddScoped<IDrugRepository, DrugRepository>();
      serviceCollection.AddScoped<IUserRepository, UserRepository>();

      return serviceCollection;
    }
  }
}
