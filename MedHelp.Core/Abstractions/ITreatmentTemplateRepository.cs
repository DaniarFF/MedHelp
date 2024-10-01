using MedHelp.Core.Entities;
using MedHelp.Core.Models;

namespace MedHelp.DBase
{
  /// <summary>
  /// Репозиторий шаблонов лечения
  /// </summary>
  public interface ITreatmentTemplateRepository
  {
    /// <summary>
    /// Получить все шаблоны лечения пользователя.
    /// </summary>
    /// <param name="user"></param>
    /// <returns><see cref="TreatmentTemplateEntity"/></returns>
    Task<TreatmentTemplateEntity> Get(User user);

    /// <summary>
    /// Получить все существующие шаблоны.
    /// </summary>
    /// <returns>Коллекцию <see cref="TreatmentTemplateEntity"/></returns>
    Task<IQueryable<TreatmentTemplateEntity>> GetAll();
  }
}