using System.Globalization;

namespace MedHelp.Core.Models
{
  /// <summary>
  /// Представляет заболевание с его характеристиками и рекомендуемым лечением.
  /// </summary>
  public class Disease
  {
    /// <summary>
    /// Уникальный идентификатор заболевания.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название заболевания.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Симптомы заболевания.
    /// </summary>
    public string Symptoms { get; set; }

    /// <summary>
    /// Рекомендации по лечению заболевания.
    /// </summary>
    public string Recomendations { get; set; }

    /// <summary>
    /// Отличительные признаки заболевания.
    /// </summary>
    public string DistinctiveSigns { get; set; }

    /// <summary>
    /// Список лекарств, рекомендуемых для лечения заболевания.
    /// </summary>
    public IEnumerable<Drug> Treatment { get; set; }
  }
}
