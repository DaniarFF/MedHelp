using MedHelp.Core.Entities;

namespace MedHelp.DBase
{
  /// <summary>
  /// Репозиторий диагнозов.
  /// </summary>
  public interface IDiseaseRepository
  {
    /// <summary>
    /// Получить все обьекты болезней.
    /// </summary>
    /// <returns>Коллекцию <see cref="DiseaseEntity"/></returns>
    IQueryable<DiseaseEntity> GetAll();
    
    /// <summary>
    /// Добавить новую болезнь.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task Add(DiseaseEntity entity); 
    
    /// <summary>
    /// Обновить информацию о болезни.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task Update(DiseaseEntity entity); 
    
    /// <summary>
    /// Удалить болезнь.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task Delete(int id);  
  }
}
