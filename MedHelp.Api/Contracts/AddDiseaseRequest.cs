namespace MedHelp.Api.Contracts;

/// <summary>
///   Запрос на добавление нового заболевания.
/// </summary>
public class AddDiseaseRequest
{
  public int Id { get; set; }

  public string Name { get; set; }

  public string Symptoms { get; set; }

  public string Recomendations { get; set; }

  public string DistinctiveSigns { get; set; }
}
