using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Common;

namespace ProductCatalog.Infrastructure.Persistence.Configurations;

public class BaseProductConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseIdEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureKeys(builder);
        ConfigureEntity(builder);
    }

    protected virtual void ConfigureKeys(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }

    protected virtual void ConfigureEntity(EntityTypeBuilder<TEntity> builder)
    {
        builder.Ignore(e => e.DomainEvents);
    }
}