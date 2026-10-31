namespace EWallet.DAL.Entities;

public class VendorSubscriber
{
    public int Id { get; set; }
    public int VendorId { get; set; }
    public ServiceVendor? Vendor { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}
