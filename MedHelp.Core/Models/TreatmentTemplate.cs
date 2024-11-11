using MedHelp.Core.Entities;

namespace MedHelp.Core.Models
{
  /// <summary>
  /// Шаблон лечения, связанный с конкретным заболеванием и пользователем.
  /// </summary>
  public class TreatmentTemplate
  {
    /// <summary>
    /// Уникальный идентификатор шаблона лечения.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название шаблона лечения.
    /// </summary>
    public string TemplateName { get; set; }

    /// <summary>
    /// Заболевание, для которого предназначен данный шаблон лечения.
    /// </summary>
    public Disease Disease { get; set; }

    /// <summary>
    /// Пользователь, создавший или связанный с данным шаблоном лечения.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Список лекарств, включенных в данный шаблон лечения.
    /// </summary>
    public IEnumerable<Drug> TreatmentDrugs { get; set; }
  }
}
