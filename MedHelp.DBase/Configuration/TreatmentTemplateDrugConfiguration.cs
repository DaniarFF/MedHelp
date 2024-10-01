using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedHelp.DBase.Configuration
{
  public class TreatmentTemplateDrugConfiguration : IEntityTypeConfiguration<TreatmentTemplateDrugEntity>
  {
    public void Configure(EntityTypeBuilder<TreatmentTemplateDrugEntity> builder)
    {
      builder.Property(ttd => ttd.Description).HasMaxLength(100).IsRequired(false); 
    }
  }
}