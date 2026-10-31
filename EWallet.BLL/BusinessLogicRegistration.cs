using EWallet.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EWallet.BLL;

public static class BusinessLogicRegistration
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IProfileService, ProfileService>();

        return services;
    }
}
