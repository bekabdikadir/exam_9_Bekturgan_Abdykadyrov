using System.Security.Claims;
using EWallet.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EWallet.Web.Infrastructure;

public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public const string AccountNumberClaim = "AccountNumber";

    public AppClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim(AccountNumberClaim, user.AccountNumber ?? string.Empty));
        return identity;
    }
}
