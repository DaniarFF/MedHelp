using MedHelp.Core.Models;
using PRTelegramBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.TelegramBot.Cache
{
  /// <summary>
  /// Кеш для пошагового выполнения команд бота
  /// </summary>
  public class BotCache : ITelegramCache
  {
    public long Id { get; set; }
    public int UserId { get; set; }
    public string DoctorName { get; set; }
    public List<Disease> Diseases {  get; set; } 
    public List<Drug> Drugs { get; set; }
    public string OtherData { get; set; }

    public bool ClearData()
    {
      Id = 0;
      UserId = 0; 
      OtherData = string.Empty;
      Diseases = new List<Disease>();
      DoctorName = string.Empty;
      Drugs = new List<Drug>(); 
      return true;
    }
  }
}
