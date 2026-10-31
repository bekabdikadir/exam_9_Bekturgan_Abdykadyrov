using EWallet.BLL.Dto;
using EWallet.DAL.Repositories;

namespace EWallet.BLL.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _uow;

    public TransactionService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<TransactionDto>> GetAsync(string userId, DateTime? dateFrom, DateTime? dateTo)
    {
        DateTime? from = dateFrom.HasValue
            ? DateTime.SpecifyKind(dateFrom.Value.Date, DateTimeKind.Utc)
            : null;

        DateTime? to = dateTo.HasValue
            ? DateTime.SpecifyKind(dateTo.Value.Date.AddDays(1), DateTimeKind.Utc)
            : null;

        var items = await _uow.Transactions.GetForUserAsync(userId, from, to);

        return items
            .Select(t => new TransactionDto(t.CreatedAt, t.Type, t.Amount, t.Counterparty))
            .ToList();
    }
}
