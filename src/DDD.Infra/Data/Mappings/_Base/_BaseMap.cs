using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDD.Domain.Entities.Base;

namespace DDD.Infra.Data.Mappings.Base
{
  public class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
  {
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
      builder.HasKey(c => c.Id);

      builder.Property(c => c.Id).IsRequired().HasColumnName("Id");
    }
  }
}