using EWallet.BLL.Dto;
using EWallet.DAL.Entities;
using EWallet.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace EWallet.BLL.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(IUnitOfWork uow, ILogger<PaymentService> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<IReadOnlyList<VendorDto>> GetVendorsAsync()
    {
        var vendors = await _uow.Vendors.GetAllOrderedAsync();
        return vendors.Select(v => new VendorDto(v.Id, v.Name)).ToList();
    }

    public async Task<OperationResult> PayAsync(string userId, int vendorId, string identifier, decimal amount)
    {
        if (!WalletDefaults.IsAmountValid(amount))
        {
            return OperationResult.Fail("Error_AmountInvalid");
        }

        var requisite = (identifier ?? string.Empty).Trim();

        try
        {
            return await _uow.ExecuteInTransactionAsync(async () =>
            {
                var user = await _uow.Users.GetByIdAsync(userId);
                if (user is null)
                {
                    return OperationResult.Fail("Error_Unexpected");
                }

                var vendor = await _uow.Vendors.GetByIdAsync(vendorId);
                if (vendor is null)
                {
                    return OperationResult.Fail("Error_VendorNotFound");
                }

                var subscriber = await _uow.Subscribers.GetAsync(vendorId, requisite);
                if (subscriber is null)
                {
                    return OperationResult.Fail("Error_SubscriberNotFound");
                }

                if (user.Balance < amount)
                {
                    return OperationResult.Fail("Error_InsufficientFunds");
                }

                user.Balance -= amount;
                subscriber.Balance += amount;

                await _uow.Transactions.AddAsync(new WalletTransaction
                {
                    UserId = user.Id,
                    Type = TransactionType.Payment,
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow,
                    Counterparty = $"{vendor.Name} ({subscriber.Identifier})"
                });

                await _uow.SaveChangesAsync();

                return OperationResult.Ok("Success_Payment", user.Balance, amount, vendor.Name, subscriber.Identifier);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Payment failed");
            return OperationResult.Fail("Error_Unexpected");
        }
    }
}
