using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDD.Domain.Entities;
using DDD.Infra.Data.Mappings.Base;

namespace DDD.Infra.Data.Mappings
{
  public class UserMap : BaseMap<User>
  {
    public override void Configure(EntityTypeBuilder<User> builder)
    {
      base.Configure(builder);
      builder.ToTable("Users");
      builder.Property(c => c.Email).IsRequired().HasColumnName("Email").HasMaxLength(255);
      builder.Property(c => c.Password).IsRequired().HasColumnName("Password");
      builder.Property(c => c.Name).IsRequired().HasColumnName("Name").HasMaxLength(255);
      builder.Property(c => c.RefreshToken).HasColumnName("Refresh_Token");
      builder.Property(c => c.ExpiresAt).HasColumnName("Expires_At");

      builder.HasIndex(c => c.Email).IsUnique();
    }
  }
}