using EWallet.BLL.Dto;
using EWallet.DAL.Repositories;

namespace EWallet.BLL.Services;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _uow;

    public ProfileService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ProfileDto?> GetProfileAsync(string userId)
    {
        var user = await _uow.Users.GetByIdAsync(userId);
        if (user is null)
        {
            return null;
        }

        var subscribers = await _uow.Subscribers.GetByUserAsync(userId);
        var requisites = subscribers
            .Select(s => new RequisiteDto(s.Id, s.VendorId, s.Vendor?.Name ?? string.Empty, s.Identifier, s.Balance))
            .ToList();

        return new ProfileDto(
            user.UserName ?? string.Empty,
            user.Email,
            user.AccountNumber ?? string.Empty,
            user.Balance,
            requisites);
    }
}
