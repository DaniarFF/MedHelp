using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static iText.IO.Util.IntHashtable;

namespace MedHelp.DBase.Configuration
{
  public class DiseaseConfiguration : IEntityTypeConfiguration<DiseaseEntity>
  {
    public void Configure(EntityTypeBuilder<DiseaseEntity> builder)
    {
      builder.Property(d => d.Id).IsRequired();
      builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
      builder.Property(d => d.Symptoms).HasMaxLength(500).IsRequired();
      builder.Property(d => d.Recomendations).HasMaxLength(500).IsRequired(false);
      builder.Property(d => d.DistinctiveSigns).IsRequired(false).HasMaxLength(500);
      builder.HasMany(d => d.DiseaseDrugs).WithOne(dd => dd.Disease).HasForeignKey(dd => dd.DiseaseId);
      builder.HasMany(d => d.TreatmentTemplates).WithOne(tt => tt.Disease).HasForeignKey(tt => tt.DiseaseId);
    }
  }
}
