using MedHelp.Core.Abstractions;
using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using MedHelp.DBase;

namespace MedHelp.Core.Services
{
  public class DiseaseDrugService 
  {
    private readonly IDiseaseDrugRepository diseaseDrugRepository;

    public async Task Add(DiseaseDrug diseaseDrug)
    {
      var entity = new DiseaseDrugEntity()
      {
        DiseaseId = diseaseDrug.DiseaseId,
        DrugId = diseaseDrug.DrugId,
        Peculiarity = diseaseDrug.Peculiarity
      };

      await diseaseDrugRepository.Add(entity);
    }

    public DiseaseDrugService(IDiseaseDrugRepository diseaseDrugRepository)
    {
      this.diseaseDrugRepository = diseaseDrugRepository;
    }
  }
}
