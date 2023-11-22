using Market.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Shared.Infrastructure.Persistance.Configuration;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseIdEntity
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