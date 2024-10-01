using MedHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedHelp.DBase.Configuration
{
  public class DrugConfiguration : IEntityTypeConfiguration<DrugEntity>
  {
    public void Configure(EntityTypeBuilder<DrugEntity> builder)
    {
      builder.Property(c => c.Id).IsRequired();
      builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
      builder.Property(c => c.Recipe).HasMaxLength(500).IsRequired();
      builder.Property(c => c.GroupId).IsRequired(false);

      builder.HasMany(c => c.DiseaseDrugs)
        .WithOne(dd => dd.Drug)
        .HasForeignKey(dd => dd.DrugId);

      builder.HasOne(c => c.DrugGroup)
        .WithMany(gd => gd.Drugs)
        .HasForeignKey(c => c.GroupId);
    }
  }
}