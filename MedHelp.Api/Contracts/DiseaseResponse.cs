namespace MedHelp.Api.Contracts;

/// <summary>
///   Ответ на запрос получения информации о заболевании.
/// </summary>
public class DiseaseResponse
{
  public DiseaseResponse(string name, string symptoms, string recomendations, string distinctiveSigns)
  {
    Name = name;
    Symptoms = symptoms;
    Recomendations = recomendations;
    DistinctiveSigns = distinctiveSigns;
  }

  public int Id { get; set; }

  public string Name { get; set; }

  public string Symptoms { get; set; }

  public string Recomendations { get; set; }

  public string DistinctiveSigns { get; set; }
}