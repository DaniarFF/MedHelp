namespace MedHelp.Core.Entities
{
  /// <summary>
  /// Cущность лекарственного препарата, содержащую информацию о его названии, рецепте и ссылке на РЛС.
  /// </summary>
  public class DrugEntity
  {
    /// <summary>
    /// Уникальный идентификатор препарата.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название препарата.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание рецепта препарата.
    /// </summary>
    public string Recipe { get; set; }

    /// <summary>
    /// Ссылка на информацию о препарате в РЛС (Регистр Лекарственных Средств).
    /// </summary>
    public string RlsLink { get; set; }

    /// <summary>
    /// Коллекция связей между заболеваниями и лекарствами.
    /// </summary>
    public IQueryable<DiseaseDrugEntity> DiseaseDrugs { get; set; }

    /// <summary>
    /// Идентификатор группы препаратов, к которой относится данный препарат.
    /// </summary>
    public int? GroupId { get; set; }

    /// <summary>
    /// Группа препаратов, к которой относится данный препарат.
    /// </summary>
    public DrugGroupEntity DrugGroup { get; set; }
  }
}
