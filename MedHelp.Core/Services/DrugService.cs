using MedHelp.Core.Abstractions;
using MedHelp.Core.Models;
using MedHelp.DBase;

namespace MedHelp.Core.Services
{
  public class DrugService : IDrugService
  {
    private readonly IDrugRepository recipeRepository;

    public async Task<Drug> Get(string drugName)
    {
      var entity = (await recipeRepository.Get(drugName)).ToList();

      Drug drug = new Drug()
      {
        Id = entity[0].Id,
        GroupId = entity[0].GroupId,
        Name = entity[0].Name,
        Recipe = entity[0].Recipe,
      };

      return drug;
    }

    public async Task<IEnumerable<Drug>> GetAll()
    {
      var entities = await recipeRepository.GetAll();

      List<Drug> drugs = entities.Select(drug => new Drug()
      {
        Id = drug.Id,
        Name = drug.Name,
        Recipe = drug.Recipe,
        RlsLink = drug.RlsLink,
        GroupId = drug.GroupId,
      })
        .ToList();

      return drugs;
    }

    public DrugService(IDrugRepository recipeRepository)
    {
      this.recipeRepository = recipeRepository;
    }
  }
}
