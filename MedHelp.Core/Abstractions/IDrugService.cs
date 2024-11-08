using MedHelp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Core.Abstractions
{
  /// <summary>
  /// Cервис для получения препаратов
  /// </summary>
  public interface IDrugService
  {
    /// <summary>
    /// Получает все препараты.
    /// </summary>
    /// <returns>Список всех существующих препаратов.</returns>
    IEnumerable<Drug> GetAll();

    /// <summary>
    /// Получает препарат по названию препарата.
    /// </summary>
    /// <param name="drugName">Название препарата</param>
    /// <returns>Возвращает <see cref="Drug"/></returns>
    Drug Get(string drugName);

    /// <summary>
    /// Добавляет препарат.
    /// </summary>
    /// <param name="drug">Препарат</param>
    /// <returns></returns>
    Task Add(Drug drug);

    /// <summary>
    /// Удаляет препарат.
    /// </summary>
    /// <returns></returns>
    Task Delete(int id);
  }
}
