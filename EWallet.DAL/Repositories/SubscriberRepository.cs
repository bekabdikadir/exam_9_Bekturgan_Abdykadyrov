using EWallet.DAL.Data;
using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class SubscriberRepository : GenericRepository<VendorSubscriber>, ISubscriberRepository
{
    public SubscriberRepository(AppDbContext context) : base(context)
    {
    }

    public Task<VendorSubscriber?> GetAsync(int vendorId, string identifier)
    {
        return Set.FirstOrDefaultAsync(s => s.VendorId == vendorId && s.Identifier == identifier);
    }

    public async Task<IReadOnlyList<VendorSubscriber>> GetByUserAsync(string userId)
    {
        return await Set.AsNoTracking()
            .Include(s => s.Vendor)
            .Where(s => s.UserId == userId)
            .OrderBy(s => s.Vendor!.Name)
            .ThenBy(s => s.Identifier)
            .ToListAsync();
    }
}
