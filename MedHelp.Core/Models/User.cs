using MedHelp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Core.Models
{
  /// <summary>
  /// Представляет пользователя.
  /// </summary>
  public class User
  {
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Имя пользователя в Telegram.
    /// </summary>
    public string TelegramUserName { get; set; }

    /// <summary>
    /// Уникальный идентификатор пользователя в Telegram.
    /// </summary>
    public long TelegramId { get; set; }

    /// <summary>
    /// Список шаблонов лечения, созданных пользователем.
    /// </summary>
    public IEnumerable<TreatmentTemplate> TreatmentTemplates { get; set; }
  }

}
