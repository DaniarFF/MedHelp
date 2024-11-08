using MedHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedHelp.DBase
{
  public class DiseaseRepository : IDiseaseRepository
  {
    private readonly AppDbContext appDbContex;

    public DiseaseRepository(AppDbContext applicationDbContex)
    {
      appDbContex = applicationDbContex;
    }

    public IQueryable<DiseaseEntity> GetAll()
    {
      var query = appDbContex.Diseases.Include(x => x.DiseaseDrugs);

      return query;
    }

    public async Task Add(DiseaseEntity entity)
    {
      if (entity == null) return;

      appDbContex.Diseases.Add(entity);
      await appDbContex.SaveChangesAsync();
    }

    public async Task Update(DiseaseEntity entity)
    {
      if (entity == null) return;

      appDbContex.Diseases.Update(entity);
      await appDbContex.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
      var disease = await appDbContex.Diseases.FindAsync(id);
      if (disease == null) return;

      appDbContex.Diseases.Remove(disease);
      await appDbContex.SaveChangesAsync();
    }
  }
}
