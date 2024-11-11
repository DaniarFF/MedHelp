using MedHelp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Core.Entities
{
  /// <summary>
  /// Cвязь между <see cref="DrugEntity"/> и <see cref="DiseaseEntity"/>
  /// </summary>
  public class DiseaseDrugEntity
  {
    /// <summary>
    /// Идентификатор связи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор заболевания.
    /// </summary>
    public int DiseaseId { get; set; }

    /// <summary>
    /// Заболевание.
    /// </summary>
    public DiseaseEntity Disease { get; set; }

    /// <summary>
    /// Идентификатор препарата 
    /// </summary>
    public int DrugId { get; set; }

    /// <summary>
    /// Препарат.
    /// </summary>
    public DrugEntity Drug { get; set; }

    /// <summary>
    /// Особые указания по применению лекарственного препарата в контексте данного заболевания.
    /// </summary>
    public string Peculiarity { get; set; }  
  }
}
