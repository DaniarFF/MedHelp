using MedHelp.Core.Entities;
using MedHelp.Core.Services;
using MedHelp.DBase;
using Moq;
using NUnit.Framework;

namespace MedHelp.Tests;

/// <summary>
///   Тесты сервиса препаратов
/// </summary>
[TestFixture]
public class DrugServiceTest
{
  [OneTimeSetUp]
  public void OneTimeSetup()
  {
    mockDrugRepository = new Mock<IDrugRepository>();
    drugService = new DrugService(mockDrugRepository.Object);
  }

  [OneTimeTearDown]
  public void OneTimeTearDown()
  {
    mockDrugRepository = null;
    drugService = null;
  }

  private Mock<IDrugRepository> mockDrugRepository;
  private DrugService drugService;

  [Test]
  public void GetAll_ReturnsListOfDrugs()
  {
    var drugs = new List<DrugEntity>
    {
      new() { Id = 1, Name = "Парацетамол", GroupId = 1 },
      new() { Id = 1, Name = "Ибупрофен", GroupId = 1 },
      new() { Id = 1, Name = "Амоксициллин", GroupId = 2 }
    };
    mockDrugRepository.Setup(repo => repo.GetAll()).Returns(drugs.AsQueryable());

    var result = drugService.GetAll();

    Assert.That(result.Count(), Is.EqualTo(3));
    Assert.That(result.First().Name, Is.EqualTo("Парацетамол"));
  }

  [TestCase("Парацетамол", ExpectedResult = "Парацетамол")]
  [TestCase("Ибупрофен", ExpectedResult = "Ибупрофен")]
  [TestCase(" ", ExpectedResult = null)]
  public string Get_GetDrugByName_ReturnsDrug(string name)
  {
    var drugs = new List<DrugEntity>
    {
      new() { Id = 1, Name = "Парацетамол", GroupId = 1 },
      new() { Id = 1, Name = "Ибупрофен", GroupId = 1 },
      new() { Id = 1, Name = "Амоксициллин", GroupId = 2 }
    };

    mockDrugRepository.Setup(repo => repo.GetAll()).Returns(drugs.AsQueryable());

    var result = drugService.Get(name);
    if (result == null) return null;

    return result.Name;
  }
}