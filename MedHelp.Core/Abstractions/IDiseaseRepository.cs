using MedHelp.Core.Entities;

namespace MedHelp.DBase
{
  /// <summary>
  /// Репозиторий диагнозов.
  /// </summary>
  public interface IDiseaseRepository
  {
    /// <summary>
    /// Получить обьект болезни по названию.
    /// </summary>
    /// <param name="diseaseName"></param>
    /// <returns><see cref="DiseaseEntity"/></returns>
    Task<DiseaseEntity> Get(string diseaseName);

    /// <summary>
    /// Получить все обьекты болезней.
    /// </summary>
    /// <returns>Коллекцию <see cref="DiseaseEntity"/> </returns>
    Task<IQueryable<DiseaseEntity>> GetAll();
  }
}