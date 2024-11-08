﻿using MedHelp.Core.Entities;

namespace MedHelp.DBase
{
  /// <summary>
  /// Репозиторий препаратов.
  /// </summary>
  public interface IDrugRepository
  {
    /// <summary>
    /// Получить коллекцию всех препаратов.
    /// </summary>
    /// <returns>Коллекция <see cref="DrugEntity"/></returns>
    IQueryable<DrugEntity> GetAll();

    /// <summary>
    /// Добавить препарат.
    /// </summary>
    /// <param name="drugEntity"></param>
    Task Add(DrugEntity drugEntity);

    /// <summary>
    /// Удалить препарат.
    /// </summary>
    Task Delete(int id);

    /// <summary>
    ///  Изменить препарат.
    /// </summary>
    Task Update(DrugEntity drugEntity);
  }
}
