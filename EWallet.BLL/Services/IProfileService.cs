using EWallet.BLL.Dto;

namespace EWallet.BLL.Services;

public interface IProfileService
{
    Task<ProfileDto?> GetProfileAsync(string userId);
}
