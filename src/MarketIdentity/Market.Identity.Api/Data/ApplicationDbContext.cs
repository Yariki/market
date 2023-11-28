using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Market.Identity.Api.Data;

public class ApplicationDbContext : IdentityDbContext<AuthUser, AuthRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CardInfo> CardInfos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AuthUser>(b =>
        {
            var cards = b.Metadata.FindNavigation(nameof(AuthUser.Cards));
            cards.SetPropertyAccessMode(PropertyAccessMode.Field);

            b.Property(u => u.FirstName)
            .HasDefaultValue(string.Empty)
            .HasMaxLength(256);

            b.Property(u => u.LastName)
            .HasDefaultValue(string.Empty)
            .HasMaxLength(256);

        });

        builder.Entity<CardInfo>(b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Name).HasMaxLength(255);
            b.Property(e => e.CardNumber).HasMaxLength(16);
            b.Property(e => e.CardHolderName).HasMaxLength(255);
            b.Property(e => e.ExpirationMonth);
            b.Property(e => e.ExpirationYear);
            b.Property(e => e.Cvv);
            
            b.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);
            
        });
        builder.UseOpenIddict();
    }
}
