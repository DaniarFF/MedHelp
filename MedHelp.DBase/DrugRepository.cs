using MedHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedHelp.DBase
{
  public class DrugRepository : IDrugRepository
  {

    private readonly AppDbContext appDbContex;
    private readonly ILogger logger;

    public DrugRepository(AppDbContext applicationDbContex, ILogger<DrugRepository> logger)
    {
      appDbContex = applicationDbContex;
      this.logger = logger;
    }

    public async Task<IQueryable<DrugEntity>> Get(string drugName)
    {
      var query = appDbContex.Drugs.Where(x => x.Name == drugName);

      return query;
    }
    public async Task<IQueryable<DrugEntity>> GetAll()
    {
      try
      {
        var query = appDbContex.Drugs.Include(x => x.DrugGroup).Include(x => x.DiseaseDrugs);

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
