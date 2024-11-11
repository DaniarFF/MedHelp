using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static iText.IO.Util.IntHashtable;

namespace MedHelp.DBase.Configuration
{
  public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
  {
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
      builder.Property(u => u.Id).IsRequired();
      builder.Property(u => u.Name).HasMaxLength(50);
      builder.Property(u => u.TelegramId).IsRequired();
    }
  }
}
