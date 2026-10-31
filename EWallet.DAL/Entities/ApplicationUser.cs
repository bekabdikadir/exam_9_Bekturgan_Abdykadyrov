using Microsoft.AspNetCore.Identity;

namespace EWallet.DAL.Entities;

public class ApplicationUser : IdentityUser
{
    public string? AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public ICollection<WalletTransaction> Transactions { get; set; } = new List<WalletTransaction>();
}
