using MedHelp.Core.Entities;

namespace MedHelp.DBase;

public class TreatmentTemplateRepository : ITreatmentTemplateRepository
{
  private readonly AppDbContext appDbContex;

  public TreatmentTemplateRepository(AppDbContext applicationDbContex)
  {
    appDbContex = applicationDbContex;
  }

  public IQueryable<TreatmentTemplateEntity> GetAll()
  {
    var query = appDbContex.TreatmentsTemplates.AsQueryable();

    return query;
  }

  public async Task Add(TreatmentTemplateEntity template)
  {
    if (template == null) return;

    appDbContex.TreatmentsTemplates.Add(template);
    await appDbContex.SaveChangesAsync();
  }

  public async Task Update(TreatmentTemplateEntity template)
  {
    if (template == null) return;

    appDbContex.TreatmentsTemplates.Update(template);
    await appDbContex.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var template = await appDbContex.TreatmentsTemplates.FindAsync(id);
    if (template == null) return;

    appDbContex.TreatmentsTemplates.Remove(template);
    await appDbContex.SaveChangesAsync();
  }
}