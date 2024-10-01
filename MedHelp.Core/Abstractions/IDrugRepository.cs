using MedHelp.Core.Entities;

namespace MedHelp.DBase
{
  /// <summary>
  /// Репозиторий препаратов.
  /// </summary>
  public interface IDrugRepository
  {
    /// <summary>
    /// Получить обьект препарата по названию.
    /// </summary>
    /// <param name="drugName"></param>
    /// <returns><see cref="DrugEntity"/></returns>
    Task<IQueryable<DrugEntity>> Get(string drugName);

    /// <summary>
    /// Получить коллекцию всех препаратов.
    /// </summary>
    /// <returns>Коллекция <see cref="DrugEntity"/></returns>
    Task<IQueryable<DrugEntity>> GetAll();
  }
}