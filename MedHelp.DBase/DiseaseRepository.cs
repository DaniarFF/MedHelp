using MedHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedHelp.DBase
{
  public class DiseaseRepository : IDiseaseRepository
  {
    private readonly AppDbContext appDbContex;
    private readonly ILogger logger;

    public DiseaseRepository(AppDbContext applicationDbContex, ILogger<DiseaseRepository> logger)
    {
      appDbContex = applicationDbContex;
      this.logger = logger;
    }

    public async Task<DiseaseEntity> Get(string drugName)
    {
      var query = appDbContex.Diseases
        .Include(dd => dd.DiseaseDrugs)
        .ThenInclude(dd => dd.Drug)
        .FirstOrDefault(x => x.Name == drugName);

      return query;
    }

    public async Task<IQueryable<DiseaseEntity>> GetAll()
    {
      try
      {
        var query = appDbContex.Diseases.Include(x => x.DiseaseDrugs);

        return query;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw;
      }
    }
  }
}
