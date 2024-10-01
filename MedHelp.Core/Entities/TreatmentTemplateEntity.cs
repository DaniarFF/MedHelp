namespace MedHelp.Core.Entities
{
  /// <summary>
  /// Представляет шаблон лечения, связанный с конкретным заболеванием и пользователем.
  /// </summary>
  public class TreatmentTemplateEntity
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
    /// Идентификатор заболевания, к которому относится данный шаблон лечения.
    /// </summary>
    public int DiseaseId { get; set; }

    /// <summary>
    /// Заболевание, для которого создан этот шаблон лечения.
    /// </summary>
    public DiseaseEntity Disease { get; set; }

    /// <summary>
    /// Идентификатор пользователя, создавшего данный шаблон лечения.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Пользователь, создавший этот шаблон лечения.
    /// </summary>
    public UserEntity User { get; set; }

    /// <summary>
    /// Коллекция лекарственных препаратов, входящих в данный шаблон лечения.
    /// </summary>
    public IEnumerable<TreatmentTemplateDrugEntity> TreatmentDrugs { get; set; }
  }
}