using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedHelp.DBase.Configuration
{
  public class TreatmentTemplateConfiguration : IEntityTypeConfiguration<TreatmentTemplateEntity>
  {
    public void Configure(EntityTypeBuilder<TreatmentTemplateEntity> builder)
    {
      builder.Property(tt => tt.Id).IsRequired();
      builder.Property(tt => tt.TemplateName).HasMaxLength(20);
      builder.HasOne(d => d.Disease)
        .WithMany(tt => tt.TreatmentTemplates)
        .HasForeignKey(tt => tt.DiseaseId);

      builder.HasOne(tt => tt.User)
        .WithMany(u => u.TreatmentTemplates)
        .HasForeignKey(tt => tt.UserId);

      builder.HasMany(tt => tt.TreatmentDrugs)
        .WithOne(ttd => ttd.TreatmentTemplate)
        .HasForeignKey(ttd => ttd.TreatmentTemplateId);
    }
  }
}
