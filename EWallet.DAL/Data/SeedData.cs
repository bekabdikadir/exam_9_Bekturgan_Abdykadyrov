using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Data;

public static class SeedData
{
    public static void Apply(ModelBuilder builder)
    {
        builder.Entity<ServiceVendor>().HasData(
            new ServiceVendor { Id = 1, Name = "MegaCom", IdentifierPrefix = "0555", IdentifierDigits = 6 },
            new ServiceVendor { Id = 2, Name = "O!", IdentifierPrefix = "0700", IdentifierDigits = 6 },
            new ServiceVendor { Id = 3, Name = "Beeline", IdentifierPrefix = "0770", IdentifierDigits = 6 },
            new ServiceVendor { Id = 4, Name = "Saima Telecom", IdentifierPrefix = "SAIMA-", IdentifierDigits = 5 },
            new ServiceVendor { Id = 5, Name = "Elcat", IdentifierPrefix = "ELCAT-", IdentifierDigits = 5 });

        builder.Entity<VendorSubscriber>().HasData(
            new VendorSubscriber { Id = 1, VendorId = 1, Identifier = "0555100200", Balance = 150m },
            new VendorSubscriber { Id = 2, VendorId = 1, Identifier = "0555300400", Balance = 20m },
            new VendorSubscriber { Id = 3, VendorId = 1, Identifier = "0550777888", Balance = 0m },
            new VendorSubscriber { Id = 4, VendorId = 2, Identifier = "0700111222", Balance = 310m },
            new VendorSubscriber { Id = 5, VendorId = 2, Identifier = "0700333444", Balance = 45m },
            new VendorSubscriber { Id = 6, VendorId = 2, Identifier = "0709555666", Balance = 5m },
            new VendorSubscriber { Id = 7, VendorId = 3, Identifier = "0770123456", Balance = 80m },
            new VendorSubscriber { Id = 8, VendorId = 3, Identifier = "0770654321", Balance = 600m },
            new VendorSubscriber { Id = 9, VendorId = 4, Identifier = "SAIMA-10001", Balance = 1200m },
            new VendorSubscriber { Id = 10, VendorId = 4, Identifier = "SAIMA-10002", Balance = 0m },
            new VendorSubscriber { Id = 11, VendorId = 5, Identifier = "ELCAT-20001", Balance = 990m },
            new VendorSubscriber { Id = 12, VendorId = 5, Identifier = "ELCAT-20002", Balance = 15m });
    }
}
