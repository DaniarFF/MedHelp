using MedHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static iText.IO.Util.IntHashtable;

namespace MedHelp.DBase
{
  public class DrugRepository : IDrugRepository
  {
    private readonly AppDbContext appDbContex;

    public IQueryable<DrugEntity> GetAll()
    {
      var query = appDbContex.Drugs.AsQueryable();

      return query;
    }

    public async Task Add(DrugEntity drug)
    {
      if (drug == null) return;

      appDbContex.Drugs.Add(drug);
      await appDbContex.SaveChangesAsync();
    }
    
    public async Task Update(DrugEntity drug)
    {
      if (drug == null) return;

      appDbContex.Drugs.Update(drug);
      await appDbContex.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
      var drugEntity = await appDbContex.Drugs.FindAsync(id);
      if (drugEntity == null) return;

      appDbContex.Drugs.Remove(drugEntity);
      await appDbContex.SaveChangesAsync();
    }

    public DrugRepository(AppDbContext applicationDbContex)
    {
      appDbContex = applicationDbContex;
    }
  }
}
