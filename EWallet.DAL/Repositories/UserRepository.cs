using EWallet.DAL.Data;
using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public Task<ApplicationUser?> GetByAccountNumberAsync(string accountNumber)
    {
        return Set.FirstOrDefaultAsync(u => u.AccountNumber == accountNumber);
    }

    public Task<bool> AccountNumberExistsAsync(string accountNumber)
    {
        return Set.AnyAsync(u => u.AccountNumber == accountNumber);
    }
}
