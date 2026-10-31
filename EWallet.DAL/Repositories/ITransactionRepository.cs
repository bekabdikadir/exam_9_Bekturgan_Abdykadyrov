using EWallet.DAL.Entities;

namespace EWallet.DAL.Repositories;

public interface ITransactionRepository : IRepository<WalletTransaction>
{
    Task<IReadOnlyList<WalletTransaction>> GetForUserAsync(string userId, DateTime? fromUtc, DateTime? toUtcExclusive);
}
