using EWallet.DAL.Data;
using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class TransactionRepository : GenericRepository<WalletTransaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<WalletTransaction>> GetForUserAsync(string userId, DateTime? fromUtc, DateTime? toUtcExclusive)
    {
        IQueryable<WalletTransaction> query = Set.AsNoTracking().Where(t => t.UserId == userId);

        if (fromUtc.HasValue)
        {
            query = query.Where(t => t.CreatedAt >= fromUtc.Value);
        }

        if (toUtcExclusive.HasValue)
        {
            query = query.Where(t => t.CreatedAt < toUtcExclusive.Value);
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .ThenByDescending(t => t.Id)
            .ToListAsync();
    }
}
