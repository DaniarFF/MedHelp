using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedHelp.DBase.Configuration
{
  public class DiseaseDrugConfiguration : IEntityTypeConfiguration<DiseaseDrugEntity>
  {
    public void Configure(EntityTypeBuilder<DiseaseDrugEntity> builder)
    {
      builder.Property(dd => dd.Peculiarity).HasMaxLength(200).IsRequired(false);
    }
  }
}