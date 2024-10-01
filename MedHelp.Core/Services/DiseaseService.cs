using FuzzySharp;
using MedHelp.Core.Models;
using MedHelp.DBase;


namespace MedHelp.Core.Services
{
  /// <summary>
  /// Сервис для работы с заболеваниями.
  /// </summary>
  public class DiseaseService
  {
    private readonly IDiseaseRepository diseaseRepository;

    /// <summary>
    /// Получить список всех заболеваний.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Disease>> GetAll()
    {
      var entities = await diseaseRepository.GetAll();

      List<Disease> drugs = entities.Select(disease => new Disease()
      {
        Id = disease.Id,
        Name = disease.Name,
        Recomendations = disease.Recomendations,
        Symptoms = disease.Symptoms,
        DistinctiveSigns = disease.DistinctiveSigns,
        Treatment = disease.DiseaseDrugs.Select(dd => new Drug
        {
          Name = dd.Drug.Name,
          Recipe = dd.Drug.Recipe
        })
        .ToList()
      })
        .ToList();

      return drugs;
    }

    /// <summary>
    /// Получить наиболее подходящее заболевание по списку сиптомов.
    /// </summary>
    /// <param name="symptoms">Введенные пользователем симптомы.</param>
    /// <param name="diseases">Список заболеваний</param>
    /// <returns></returns>
    public List<Disease> GetClosestDisease(string symptoms, List<Disease> diseases)
    {
      return diseases
      .Select(disease => new
      {
        Disease = disease,
        Rate = Fuzz.TokenSetRatio(disease.Symptoms, symptoms)
      })
      .Where(x => x.Rate >= 70)
      .OrderByDescending(x => x.Rate)
      .Take(2)
      .Select(x => x.Disease)
      .ToList();
    }

    public DiseaseService(IDiseaseRepository diseaseRepository)
    {
      this.diseaseRepository = diseaseRepository;
    }
  }
}
