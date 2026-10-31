using EWallet.BLL.Dto;

namespace EWallet.BLL.Services;

public interface IPaymentService
{
    Task<IReadOnlyList<VendorDto>> GetVendorsAsync();
    Task<OperationResult> PayAsync(string userId, int vendorId, string identifier, decimal amount);
}
