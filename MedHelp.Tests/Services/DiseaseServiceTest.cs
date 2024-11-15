using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using MedHelp.Core.Services;
using MedHelp.DBase;
using Moq;
using NUnit.Framework;

namespace MedHelp.Tests;

/// <summary>
///   Тесты сервиса заболеваний (DiseaseService)
/// </summary>
[TestFixture]
public class DiseaseServiceTest
{
  [OneTimeSetUp]
  public void OneTimeSetup()
  {
    mockDiseaseRepository = new Mock<IDiseaseRepository>();
    diseaseService = new DiseaseService(mockDiseaseRepository.Object);
  }

  [OneTimeTearDown]
  public void OneTimeTearDown()
  {
    mockDiseaseRepository = null;
    diseaseService = null;
  }

  private Mock<IDiseaseRepository> mockDiseaseRepository;
  private DiseaseService diseaseService;

  [Test]
  public async Task GetAll_ReturnsListOfDiseases()
  {
    var diseases = new List<DiseaseEntity>
    {
      new()
      {
        Id = 1,
        Name = "Грипп",
        Symptoms = "Кашель, насморк",
        Recomendations = "Рекомендации",
        DistinctiveSigns = "Носит сезонный характер",
        DiseaseDrugs = new List<DiseaseDrugEntity>
        {
          new() { Drug = new DrugEntity { Name = "Парацетамол", Recipe = "Принимать при высокой температуре" } },
          new() { Drug = new DrugEntity { Name = "Ибупрофен", Recipe = "Принимать при высокой температуре" } }
        }
      },
      new()
      {
        Id = 1,
        Name = "Орви",
        Symptoms = "Кашель, насморк",
        Recomendations = "Рекомендации",
        DistinctiveSigns = "Носит сезонный характер",
        DiseaseDrugs = new List<DiseaseDrugEntity>
        {
          new() { Drug = new DrugEntity { Name = "Парацетамол", Recipe = "Принимать при высокой температуре" } },
          new() { Drug = new DrugEntity { Name = "Ибупрофен", Recipe = "Принимать при высокой температуре" } }
        }
      }
    };

    mockDiseaseRepository.Setup(repo => repo.GetAll()).Returns(diseases.AsQueryable());

    var result = await diseaseService.GetAll();

    Assert.AreEqual(2, result.Count());
    Assert.AreEqual("Парацетамол", result.First().Treatment.First().Name);
  }

  [TestCase("Кашель, насморк, температура 37", ExpectedResult = true)]
  [TestCase("Болят ноги и диарея, так же распирает ягодицы", ExpectedResult = false)]
  [TestCase(" ", ExpectedResult = false)]
  public bool GetClosedDisease_CompareSymptoms_ReturnDiseasesWithHighMatchRate(string symptoms)
  {
    var disease = new List<Disease>
    {
      new() { Name = "Орви", Symptoms = "Насморк, кашель, температура 37" },
      new() { Name = "Энтеровирусная инфекция", Symptoms = "Сыпь, герпангина, температура 39" }
    };

    var mostMatchedDisease = new List<Disease>
    {
      disease[0]
    };

    var result = diseaseService.GetClosestDisease(symptoms, disease);

    if (!result.Any()) return false;

    if (mostMatchedDisease[0] == result[0])
    {
      Assert.Pass();
      return true;
    }

    Assert.Fail();
    return false;
  }
}