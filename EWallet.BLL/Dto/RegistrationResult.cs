using EWallet.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace EWallet.BLL.Dto;

public class RegistrationResult
{
    public bool Succeeded { get; init; }
    public string? AccountNumber { get; init; }
    public ApplicationUser? User { get; init; }
    public IReadOnlyList<IdentityError> Errors { get; init; } = Array.Empty<IdentityError>();
}
