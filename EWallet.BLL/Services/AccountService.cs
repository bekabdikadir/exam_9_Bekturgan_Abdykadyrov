using System.Globalization;
using System.Security.Cryptography;
using EWallet.BLL.Dto;
using EWallet.DAL.Entities;
using EWallet.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EWallet.BLL.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _uow;
    private readonly ILogger<AccountService> _logger;

    public AccountService(UserManager<ApplicationUser> userManager, IUnitOfWork uow, ILogger<AccountService> logger)
    {
        _userManager = userManager;
        _uow = uow;
        _logger = logger;
    }

    public async Task<RegistrationResult> RegisterAsync(string userName, string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName.Trim(),
            Email = email.Trim(),
            Balance = WalletDefaults.InitialBalance
        };

        var created = await _userManager.CreateAsync(user, password);
        if (!created.Succeeded)
        {
            return new RegistrationResult
            {
                Succeeded = false,
                Errors = created.Errors.ToList()
            };
        }

        try
        {
            user.AccountNumber = await GenerateUniqueAccountNumberAsync();

            var updated = await _userManager.UpdateAsync(user);
            if (!updated.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new RegistrationResult
                {
                    Succeeded = false,
                    Errors = updated.Errors.ToList()
                };
            }

            await CreateRequisitesAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration post-processing failed");
            await _userManager.DeleteAsync(user);
            return new RegistrationResult
            {
                Succeeded = false,
                Errors = new List<IdentityError> { new IdentityError { Code = "RegistrationFailed", Description = string.Empty } }
            };
        }

        return new RegistrationResult
        {
            Succeeded = true,
            AccountNumber = user.AccountNumber,
            User = user
        };
    }

    public async Task<ApplicationUser?> FindByLoginAsync(string login)
    {
        var value = (login ?? string.Empty).Trim();
        if (value.Length == 0)
        {
            return null;
        }

        if (value.Contains('@'))
        {
            return await _userManager.FindByEmailAsync(value);
        }

        return await _uow.Users.GetByAccountNumberAsync(value);
    }

    private async Task CreateRequisitesAsync(ApplicationUser user)
    {
        var vendors = await _uow.Vendors.GetAllOrderedAsync();

        foreach (var vendor in vendors)
        {
            var identifier = await GenerateUniqueIdentifierAsync(vendor);

            await _uow.Subscribers.AddAsync(new VendorSubscriber
            {
                VendorId = vendor.Id,
                UserId = user.Id,
                Identifier = identifier,
                Balance = 0m
            });
        }

        await _uow.SaveChangesAsync();
    }

    private async Task<string> GenerateUniqueIdentifierAsync(ServiceVendor vendor)
    {
        var digits = vendor.IdentifierDigits > 0 ? vendor.IdentifierDigits : 6;
        var upper = (int)Math.Pow(10, digits);
        var format = "D" + digits.ToString(CultureInfo.InvariantCulture);

        while (true)
        {
            var number = RandomNumberGenerator.GetInt32(0, upper).ToString(format, CultureInfo.InvariantCulture);
            var identifier = vendor.IdentifierPrefix + number;

            if (await _uow.Subscribers.GetAsync(vendor.Id, identifier) is null)
            {
                return identifier;
            }
        }
    }

    private async Task<string> GenerateUniqueAccountNumberAsync()
    {
        while (true)
        {
            var number = RandomNumberGenerator.GetInt32(100000, 1000000).ToString(CultureInfo.InvariantCulture);
            if (!await _uow.Users.AccountNumberExistsAsync(number))
            {
                return number;
            }
        }
    }
}
