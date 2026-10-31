namespace EWallet.DAL.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ITransactionRepository Transactions { get; }
    IVendorRepository Vendors { get; }
    ISubscriberRepository Subscribers { get; }
    Task<int> SaveChangesAsync();
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action);
}
