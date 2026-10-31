using EWallet.BLL.Dto;

namespace EWallet.BLL.Services;

public interface ITransactionService
{
    Task<IReadOnlyList<TransactionDto>> GetAsync(string userId, DateTime? dateFrom, DateTime? dateTo);
}
