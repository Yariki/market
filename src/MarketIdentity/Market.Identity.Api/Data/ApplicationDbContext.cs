using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Market.Identity.Api.Data;

public class ApplicationDbContext : IdentityDbContext<AuthUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AuthUser>(b =>
        {
            var cards = b.Metadata.FindNavigation(nameof(AuthUser.Cards));
            cards.SetPropertyAccessMode(PropertyAccessMode.Field);
            
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

    }
}
