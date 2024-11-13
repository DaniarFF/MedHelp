using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Core.Entities
{
  /// <summary>
  /// Сущность пользователя.
  /// </summary>
  public class UserEntity
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
    /// Уникальный идентификатор пользователя в Telegram.
    /// </summary>
    public long TelegramId { get; set; }
    
    /// <summary>
    ///  Логин пользователя в телеграм.
    /// </summary>
    public string TelegramUserName { get; set; }

    /// <summary>
    /// Коллекция шаблонов лечения, созданных пользователем.
    /// </summary>
    public IEnumerable<TreatmentTemplateEntity> TreatmentTemplates { get; set; }
  }
}
