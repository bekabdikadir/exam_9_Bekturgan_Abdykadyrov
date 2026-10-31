using System.Data;
using EWallet.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(
        AppDbContext context,
        IUserRepository users,
        ITransactionRepository transactions,
        IVendorRepository vendors,
        ISubscriberRepository subscribers)
    {
        _context = context;
        Users = users;
        Transactions = transactions;
        Vendors = vendors;
        Subscribers = subscribers;
    }

    public IUserRepository Users { get; }
    public ITransactionRepository Transactions { get; }
    public IVendorRepository Vendors { get; }
    public ISubscriberRepository Subscribers { get; }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            var result = await action();
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
