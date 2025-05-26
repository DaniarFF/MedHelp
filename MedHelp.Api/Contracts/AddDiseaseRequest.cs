using System.ComponentModel.DataAnnotations;

namespace MedHelp.Api.Contracts;

/// <summary>
///   Запрос на добавление нового заболевания.
/// </summary>
public class AddDiseaseRequest
{
  [Required] public string Name { get; set; }

  [Required] public string Symptoms { get; set; }

  public string Recomendations { get; set; }

  public string DistinctiveSigns { get; set; }
}