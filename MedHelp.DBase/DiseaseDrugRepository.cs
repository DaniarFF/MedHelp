using MedHelp.Core.Entities;

namespace MedHelp.DBase
{
  public class DiseaseDrugRepository : IDiseaseDrugRepository
  {
    private readonly AppDbContext appDbContex;

    public async Task Add(DiseaseDrugEntity entity)
    {
      await appDbContex.DiseaseDrugs.AddAsync(entity);
      await appDbContex.SaveChangesAsync();
    }

    public DiseaseDrugRepository(AppDbContext applicationDbContex)
    {
      appDbContex = applicationDbContex;
    }
  }
}
