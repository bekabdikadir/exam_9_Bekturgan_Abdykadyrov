using EWallet.DAL.Entities;

namespace EWallet.DAL.Repositories;

public interface IVendorRepository : IRepository<ServiceVendor>
{
    Task<IReadOnlyList<ServiceVendor>> GetAllOrderedAsync();
}
