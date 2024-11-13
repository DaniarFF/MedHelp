using MedHelp.Core.Entities;

namespace MedHelp.DBase;

public interface ITreatmentTemplateRepository
{
  /// <summary>
  /// Получить все шаблоны лечения
  /// </summary>
  /// <returns></returns>
  IQueryable<TreatmentTemplateEntity> GetAll();
  
  /// <summary>
  /// Добавить новый шаблон лечения
  /// </summary>
  /// <param name="template"></param>
  /// <returns></returns>
  Task Add(TreatmentTemplateEntity template);
  
  /// <summary>
  ///  Обновить существующий шаблон лечения
  /// </summary>
  /// <param name="template"></param>
  /// <returns></returns>
  Task Update(TreatmentTemplateEntity template);
  
  /// <summary>
  /// Удалить шаблон лечения по идентификатору
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task Delete(int id);
}