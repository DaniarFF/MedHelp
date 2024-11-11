using iText.Kernel.XMP.Options;
using MedHelp.Core.Entities;
using MedHelp.DBase.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MedHelp.DBase
{
  /// <summary>
  ///   Контекст для работы с БД PostgreSQL.
  /// </summary>
  public class AppDbContext : DbContext
  {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new DrugConfiguration());
      modelBuilder.ApplyConfiguration(new DiseaseConfiguration());
      modelBuilder.ApplyConfiguration(new DiseaseDrugConfiguration());
      modelBuilder.ApplyConfiguration(new TreatmentTemplateConfiguration());
      modelBuilder.ApplyConfiguration(new TreatmentTemplateDrugConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new DrugGroupConfiguration());
    }

    public DbSet<DrugEntity> Drugs { get; set; }
    public DbSet<DiseaseEntity> Diseases { get; set; }

    public DbSet<DiseaseDrugEntity> DiseaseDrugs { get; set; }
    public DbSet<TreatmentTemplateDrugEntity> TreatmentsTemplatesDrugs { get; set; }
    public DbSet<TreatmentTemplateEntity> TreatmentsTemplates { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<DrugGroupEntity> DrugsGroups { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
  }
}
