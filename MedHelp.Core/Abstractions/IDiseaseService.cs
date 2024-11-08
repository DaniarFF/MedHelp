using FuzzySharp;
using MedHelp.Core.Models;

namespace MedHelp.Core.Abstractions;

/// <summary>
/// Сервис для работы с заболеваниями.
/// </summary>
public interface IDiseaseService
{
  /// <summary>
  /// Получить список всех заболеваний.
  /// </summary>
  /// <returns></returns>
  Task<IEnumerable<Disease>> GetAll();

  /// <summary>
  ///  Получить наиболее подходящее заболевание по списку симптомов. 
  /// </summary>
  /// <param name="symptoms"></param>
  /// <param name="diseases"></param>
  /// <returns></returns>
  List<Disease> GetClosestDisease(string symptoms, List<Disease> diseases);
  
  /// <summary>
  /// Добавить новое заболевание.
  /// </summary>
  /// <param name="disease"></param>
  /// <returns></returns>
  Task Add(Disease disease);
  
  /// <summary>
  /// Обновить заболевание. 
  /// </summary>
  /// <param name="disease"></param>
  /// <returns></returns>
  Task Update(Disease disease); 
  
  /// <summary>
  /// Удалить заболевание по идентификатору. 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task Delete(int id); 
}
