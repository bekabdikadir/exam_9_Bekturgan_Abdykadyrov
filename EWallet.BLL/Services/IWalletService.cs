using EWallet.BLL.Dto;

namespace EWallet.BLL.Services;

public interface IWalletService
{
    Task<decimal> GetBalanceAsync(string userId);
    Task<OperationResult> TopUpAsync(string accountNumber, decimal amount);
    Task<OperationResult> TransferAsync(string senderId, string recipientAccountNumber, decimal amount);
}
