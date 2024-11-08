using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static iText.IO.Util.IntHashtable;

namespace MedHelp.DBase.Configuration
{
  public class DrugGroupConfiguration : IEntityTypeConfiguration<DrugGroupEntity>
  {
    public void Configure(EntityTypeBuilder<DrugGroupEntity> builder)
    {
      builder.Property(gd => gd.Id).IsRequired();
      builder.Property(gd => gd.Name).IsRequired().HasMaxLength(50);    
    }
  }
}
