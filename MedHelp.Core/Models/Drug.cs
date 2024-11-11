using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Core.Models
{
  /// <summary>
  /// Лекарственное средство с его характеристиками.
  /// </summary>
  public class Drug
  {
    /// <summary>
    /// Уникальный идентификатор лекарства.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название лекарства.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Рецептура лекарства.
    /// </summary>
    public string Recipe { get; set; }

    /// <summary>
    /// Ссылка на регистрацию лекарства.
    /// </summary>
    public string RlsLink { get; set; }

    /// <summary>
    /// Идентификатор группы, к которой относится лекарство.
    /// </summary>
    public int? GroupId { get; set; }
  }
}
