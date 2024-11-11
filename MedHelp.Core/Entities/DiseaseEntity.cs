namespace MedHelp.Core.Entities
{
  /// <summary>
  /// Cущность заболевания, содержащую информацию о симптомах, рекомендациях и отличительных признаках.
  /// </summary>
  public class DiseaseEntity
  {
    /// <summary>
    /// Уникальный идентификатор заболевания.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование заболевания.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание симптомов, характерных для данного заболевания.
    /// </summary>
    public string Symptoms { get; set; }

    /// <summary>
    /// Рекомендации по лечению данного заболевания.
    /// </summary>
    public string Recomendations { get; set; }

    /// <summary>
    /// Отличительные признаки заболевания, помогающие его дифференцировать.
    /// </summary>
    public string DistinctiveSigns { get; set; }

    /// <summary>
    /// Коллекция лекарств, связанных с данным заболеванием.
    /// </summary>
    public ICollection<DiseaseDrugEntity> DiseaseDrugs { get; set; }

    /// <summary>
    /// Коллекция шаблонов лечения, связанных с данным заболеванием.
    /// </summary>
    public IEnumerable<TreatmentTemplateEntity> TreatmentTemplates { get; set; }
  }
}
