using EWallet.DAL.Data;
using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class VendorRepository : GenericRepository<ServiceVendor>, IVendorRepository
{
    public VendorRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<ServiceVendor>> GetAllOrderedAsync()
    {
        return await Set.AsNoTracking().OrderBy(v => v.Name).ToListAsync();
    }
}
