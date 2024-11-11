namespace MedHelp.Core.Entities
{
  /// <summary>
  /// Связь между шаблоном лечения и лекарственным препаратом.
  /// </summary>
  public class TreatmentTemplateDrugEntity
  {
    /// <summary>
    /// Уникальный идентификатор записи.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор шаблона лечения, к которому относится данный препарат.
    /// </summary>
    public int TreatmentTemplateId { get; set; }

    /// <summary>
    /// Шаблон лечения, к которому относится данный препарат.
    /// </summary>
    public TreatmentTemplateEntity TreatmentTemplate { get; set; }

    /// <summary>
    /// Идентификатор лекарственного препарата.
    /// </summary>
    public int DrugId { get; set; }

    /// <summary>
    /// Лекарственный препарат, который включён в данный шаблон лечения.
    /// </summary>
    public DrugEntity Drug { get; set; }

    /// <summary>
    /// Описание способа применения препарата в рамках данного шаблона лечения.
    /// </summary>
    public string Description { get; set; }
  }
}
