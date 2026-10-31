using EWallet.DAL.Entities;

namespace EWallet.DAL.Repositories;

public interface ISubscriberRepository : IRepository<VendorSubscriber>
{
    Task<VendorSubscriber?> GetAsync(int vendorId, string identifier);
    Task<IReadOnlyList<VendorSubscriber>> GetByUserAsync(string userId);
}
