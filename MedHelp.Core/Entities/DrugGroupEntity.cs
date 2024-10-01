using MedHelp.Core.Models;

namespace MedHelp.Core.Entities
{
  /// <summary>
  /// Представляет группу лекарственных препаратов.
  /// </summary>
  public class DrugGroupEntity
  {
    /// <summary>
    /// Уникальный идентификатор группы препаратов.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название группы препаратов.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Коллекция лекарственных препаратов, относящихся к данной группе.
    /// </summary>
    public ICollection<DrugEntity> Drugs { get; set; }
  }
}
