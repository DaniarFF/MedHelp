using MedHelp.Core.Entities;

namespace MedHelp.DBase
{
  /// <summary>
  /// Репозиторий для хранения связей заболевания и лекарств  
  /// </summary>
  public interface IDiseaseDrugRepository
  {
    /// <summary>
    /// Добавить связь заболевания и лекарства
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task Add(DiseaseDrugEntity entity);
  }
}
