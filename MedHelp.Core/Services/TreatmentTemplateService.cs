using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using MedHelp.DBase;
using Microsoft.EntityFrameworkCore;

namespace MedHelp.Core.Services;

public class TreatmentTemplateService
{
  private readonly ITreatmentTemplateRepository repository;

  public TreatmentTemplateService(ITreatmentTemplateRepository repository)
  {
    this.repository = repository;
  }

  public IEnumerable<TreatmentTemplate> GetTemplatesByUserId(int userId)
  {
    var entities = repository.GetAll();

    var userTemplatesEntities = entities
      .Where(t => t.UserId == userId)
      .Include(t => t.TreatmentDrugs)
      .ThenInclude(td => td.Drug)
      .Include(t => t.Disease)
      .Include(t => t.User).ToList();

    var userTemplates = userTemplatesEntities.Select(t => new TreatmentTemplate
    {
      Id = t.Id,
      TemplateName = t.TemplateName,
      Disease = new Disease
      {
        Id = t.Disease.Id,
        Name = t.Disease.Name
      },
      User = new User
      {
        Id = t.User.Id,
        Name = t.User.Name
      },
      TreatmentDrugs = t.TreatmentDrugs.Select(td => new Drug
      {
        Id = td.Drug.Id,
        Name = td.Drug.Name,
        Recipe = td.Drug.Recipe,
        GroupId = td.Drug.GroupId,
        RlsLink = td.Drug.RlsLink
      }).ToList()
    });

    return userTemplates;
  }

  public void Add(TreatmentTemplate template)
  {
    var entity = new TreatmentTemplateEntity
    {
      TemplateName = template.TemplateName,
      UserId = template.User.Id,
      DiseaseId = template.Disease.Id,
      TreatmentDrugs = template.TreatmentDrugs.Select(d => new TreatmentTemplateDrugEntity
      {
        DrugId = d.Id
      }).ToList()
    };
  }

  public async Task Update(TreatmentTemplateEntity template)
  {
    await repository.Update(template);
  }

  public async Task Delete(int id)
  {
    await repository.Delete(id);
  }
}