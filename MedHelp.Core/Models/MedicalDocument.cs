using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Core.Models
{
  // <summary>
  /// Медицинский документ (рецепт) с информацией о пациенте и списком назначенных лекарств.
  /// </summary>
  public class MedicalDocument
  {
    /// <summary>
    /// Имя пациента.
    /// </summary>
    public string PatientName { get; set; }

    /// <summary>
    /// Возраст пациента.
    /// </summary>
    public string Age { get; set; }

    /// <summary>
    /// Имя врача, который заполняет документ.
    /// </summary>
    public string DoctorName { get; set; }

    /// <summary>
    /// Список лекарств, назначенных пациенту.
    /// </summary>
    public List<Drug> DrugList { get; set; }
  }
}
