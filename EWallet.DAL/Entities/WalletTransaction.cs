namespace EWallet.DAL.Entities;

public class WalletTransaction
{
    public long Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Counterparty { get; set; }
}
