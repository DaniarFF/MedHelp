using MedHelp.Core.Abstractions;
using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using MedHelp.DBase;

namespace MedHelp.Core.Services
{
  public class DrugService : IDrugService
  {
    private readonly IDrugRepository drugsRepository;

    public Drug Get(string drugName)
    {
      var entity = drugsRepository.GetAll()
        .FirstOrDefault(d => d.Name.ToUpper() == drugName.ToUpper());

      if (entity == null) return null;

      var drug = new Drug()
      {
        Id = entity.Id,
        GroupId = entity.GroupId,
        Name = entity.Name,
        Recipe = entity.Recipe,
      };

      return drug;
    }

    public IEnumerable<Drug> GetAll()
    {
      var entities = drugsRepository.GetAll();

      var drugs = entities.Select(drug => new Drug()
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

    public async Task Add(Drug drug)
    {
      var entity = new DrugEntity()
      {
        Name = drug.Name,
        GroupId = drug.GroupId,
        Recipe = drug.Recipe,
        RlsLink = drug.RlsLink,
      };

      await drugsRepository.Add(entity);
    }

    public async Task Delete(int id)
    {
      await drugsRepository.Delete(id);
    }
    
    public DrugService(IDrugRepository recipeRepository)
    {
      this.drugsRepository = recipeRepository;
    }
  }
}
