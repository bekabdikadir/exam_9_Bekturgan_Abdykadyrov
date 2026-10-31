namespace EWallet.DAL.Entities;

public class ServiceVendor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IdentifierPrefix { get; set; } = string.Empty;
    public int IdentifierDigits { get; set; }
    public ICollection<VendorSubscriber> Subscribers { get; set; } = new List<VendorSubscriber>();
}
