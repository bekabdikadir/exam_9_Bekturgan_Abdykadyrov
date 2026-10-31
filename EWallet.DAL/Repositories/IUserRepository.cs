using EWallet.DAL.Entities;

namespace EWallet.DAL.Repositories;

public interface IUserRepository : IRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetByAccountNumberAsync(string accountNumber);
    Task<bool> AccountNumberExistsAsync(string accountNumber);
}
