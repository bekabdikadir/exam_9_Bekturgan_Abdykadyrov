using EWallet.BLL.Dto;
using EWallet.DAL.Entities;
using EWallet.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace EWallet.BLL.Services;

public class WalletService : IWalletService
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<WalletService> _logger;

    public WalletService(IUnitOfWork uow, ILogger<WalletService> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<decimal> GetBalanceAsync(string userId)
    {
        var user = await _uow.Users.GetByIdAsync(userId);
        return user?.Balance ?? 0m;
    }

    public async Task<OperationResult> TopUpAsync(string accountNumber, decimal amount)
    {
        if (!WalletDefaults.IsAmountValid(amount))
        {
            return OperationResult.Fail("Error_AmountInvalid");
        }

        var number = (accountNumber ?? string.Empty).Trim();

        try
        {
            return await _uow.ExecuteInTransactionAsync(async () =>
            {
                var user = await _uow.Users.GetByAccountNumberAsync(number);
                if (user is null)
                {
                    return OperationResult.Fail("Error_AccountNotFound");
                }

                user.Balance += amount;

                await _uow.Transactions.AddAsync(new WalletTransaction
                {
                    UserId = user.Id,
                    Type = TransactionType.TopUp,
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow
                });

                await _uow.SaveChangesAsync();

                return OperationResult.Ok("Success_TopUp", user.Balance, amount, user.AccountNumber ?? string.Empty);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Top up failed");
            return OperationResult.Fail("Error_Unexpected");
        }
    }

    public async Task<OperationResult> TransferAsync(string senderId, string recipientAccountNumber, decimal amount)
    {
        if (!WalletDefaults.IsAmountValid(amount))
        {
            return OperationResult.Fail("Error_AmountInvalid");
        }

        var number = (recipientAccountNumber ?? string.Empty).Trim();

        try
        {
            return await _uow.ExecuteInTransactionAsync(async () =>
            {
                var sender = await _uow.Users.GetByIdAsync(senderId);
                if (sender is null)
                {
                    return OperationResult.Fail("Error_Unexpected");
                }

                var recipient = await _uow.Users.GetByAccountNumberAsync(number);
                if (recipient is null)
                {
                    return OperationResult.Fail("Error_RecipientNotFound");
                }

                if (recipient.Id == sender.Id)
                {
                    return OperationResult.Fail("Error_SelfTransfer");
                }

                if (sender.Balance < amount)
                {
                    return OperationResult.Fail("Error_InsufficientFunds");
                }

                var now = DateTime.UtcNow;

                sender.Balance -= amount;
                recipient.Balance += amount;

                await _uow.Transactions.AddAsync(new WalletTransaction
                {
                    UserId = sender.Id,
                    Type = TransactionType.TransferOut,
                    Amount = amount,
                    CreatedAt = now,
                    Counterparty = recipient.AccountNumber
                });

                await _uow.Transactions.AddAsync(new WalletTransaction
                {
                    UserId = recipient.Id,
                    Type = TransactionType.TransferIn,
                    Amount = amount,
                    CreatedAt = now,
                    Counterparty = sender.AccountNumber
                });

                await _uow.SaveChangesAsync();

                return OperationResult.Ok("Success_Transfer", sender.Balance, amount, recipient.AccountNumber ?? string.Empty);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transfer failed");
            return OperationResult.Fail("Error_Unexpected");
        }
    }
}
