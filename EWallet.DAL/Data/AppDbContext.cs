using EWallet.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<WalletTransaction> Transactions => Set<WalletTransaction>();
    public DbSet<ServiceVendor> Vendors => Set<ServiceVendor>();
    public DbSet<VendorSubscriber> Subscribers => Set<VendorSubscriber>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(e =>
        {
            e.Property(x => x.AccountNumber).HasMaxLength(6);
            e.HasIndex(x => x.AccountNumber).IsUnique();
            e.Property(x => x.Balance).HasPrecision(18, 2);
        });

        builder.Entity<WalletTransaction>(e =>
        {
            e.Property(x => x.Amount).HasPrecision(18, 2);
            e.Property(x => x.Counterparty).HasMaxLength(200);
            e.HasIndex(x => new { x.UserId, x.CreatedAt });
            e.HasOne(x => x.User)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ServiceVendor>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(100).IsRequired();
            e.Property(x => x.IdentifierPrefix).HasMaxLength(20).IsRequired();
        });

        builder.Entity<VendorSubscriber>(e =>
        {
            e.Property(x => x.Identifier).HasMaxLength(50).IsRequired();
            e.Property(x => x.Balance).HasPrecision(18, 2);
            e.HasIndex(x => new { x.VendorId, x.Identifier }).IsUnique();
            e.HasOne(x => x.Vendor)
                .WithMany(x => x.Subscribers)
                .HasForeignKey(x => x.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        SeedData.Apply(builder);
    }
}
